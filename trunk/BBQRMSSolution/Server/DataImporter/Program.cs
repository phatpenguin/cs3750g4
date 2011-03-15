using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using LinqToCsv;

namespace DataImporter
{
	class Program
	{
		static int Main(string[] args)
		{
			//TODO: argument validation
			var tablesFileName = args[0];
			var connectionString = args[1];
			var fi = new FileInfo(tablesFileName);
			var tablesFileDirectory = fi.DirectoryName;

			List<KeyValuePair<string,string>> tables;
			using(var fs = File.OpenRead(tablesFileName))
			{
				tables = fs.GetCsvRecords().Select(rec => new KeyValuePair<string,string>(rec[0], rec[1])).ToList();
			}
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var getColumnsCmd = CreateGetColumnsCommand(connection);
				ILookup<string, ColumnDef> tableDefs = GetTableDefs(getColumnsCmd);

				foreach (KeyValuePair<string, string> keyValuePair in tables)
				{
					int tableRowCount = 0;
					string tableName = keyValuePair.Key;
					string tableDataFileName = keyValuePair.Value;

					string dataFile = Path.Combine(tablesFileDirectory, tableDataFileName);
					fi = new FileInfo(dataFile);
					var tableDataFileDirectory = fi.DirectoryName;
					using (var fs = File.OpenRead(dataFile))
					{
						var recordEnumerator = fs.GetCsvRecords().GetEnumerator();
						recordEnumerator.MoveNext();
						CsvRecord headers = recordEnumerator.Current;

						SqlCommand insertOrUpdateCommand = MakeCommand(connection, tableDefs[tableName], headers);

						while (recordEnumerator.MoveNext())
						{
							var currentRecord = recordEnumerator.Current;
							// put this row in the database either as an insert or an update.

							ClearParameters(insertOrUpdateCommand);
							foreach (CsvField field in currentRecord.Fields)
							{
								string parameterName = "@" + headers[field.Index].ToLower();
								// handle backtick field values... which mean to retrieve the bytes from another file.
								if (field.Value.StartsWith("`") && field.Value.EndsWith("`"))
								{
									// get the data from a file
									var fieldValueFilePath = Path.Combine(tableDataFileDirectory, field.Value.Trim('`'));
									using(var stream = new FileStream(fieldValueFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
									{
										var buffer = new byte[(int)stream.Length];
										stream.Read(buffer, 0, (int) stream.Length);
										insertOrUpdateCommand.Parameters[parameterName].Value = buffer;
									}
								}
								else
								{
									insertOrUpdateCommand.Parameters[parameterName].Value = field.Value;
								}
							}
							tableRowCount += insertOrUpdateCommand.ExecuteNonQuery();
						}
						// report progress to the console.
						Console.Out.WriteLine("Table [{0}]: {1}", tableName, tableRowCount);
					}
					//TODO: delete extra rows (without failing) if the tables file says to.
				}
			}
			return 0;
		}

		private static void ClearParameters(SqlCommand insertOrUpdateCommand)
		{
			foreach (SqlParameter parameter in insertOrUpdateCommand.Parameters)
			{
				parameter.Value = null;
			}
		}

		private static SqlCommand MakeCommand(SqlConnection connection, IEnumerable<ColumnDef> columnDefs, CsvRecord headers)
		{
			//TODO: ensure that all the not-nullable columns have corresponding headers (this will include the primary key fields).
			//TODO: ensure that all the columns in headers are actually in the table.

			var toReturn = connection.CreateCommand();
			var tableName = columnDefs.First().TableName;
			
			/*
			 * update [dbo].[DinerType]
			 * set [Description] = @Description,
			 * [Code] = @Code
			 * where [Id] = @Id
			 * 
			 * insert into [dbo].[DinerType] ([Id], [Description], [Code]) select @Id, @Description, @Code where not exists(select * from [dbo].[DinerType] where [Id] = @Id)
			 */

			string setClause = GetNonPkSetClause(columnDefs, headers);

			string pkWhereClause = GetPkWhereClause(columnDefs);

			string insertColumnListClause = GetInsertColumnListClause(headers);

			string insertSelectColumnListClause = GetInsertSelectColumnListClause(headers);

			//the setClause might be an empty string when this is a linking table
			string commandText = "";
			if (!string.IsNullOrEmpty(setClause))
				commandText = string.Format("update [dbo].[{0}] {1} {2}", tableName, setClause, pkWhereClause);

			commandText += string.Format(
				@"
insert into [dbo].[{0}] ({2}) select {3} where not exists (select * from [dbo].[{0}] {1})",
				tableName, pkWhereClause, insertColumnListClause, insertSelectColumnListClause);

			if (columnDefs.Any(cd => cd.IsIdentity))
				commandText = "set identity_insert [dbo].[" + tableName + "] on\r\n" + commandText +
				              "\r\nset identity_insert [dbo].[" +
				              tableName + "] off\r\n";

			toReturn.CommandText = commandText;


			foreach (CsvField header in headers.Fields)
			{
				CsvField header1 = header;
				var columnDef = columnDefs.First(cd => string.Equals(cd.ColumnName, header1.Value, StringComparison.OrdinalIgnoreCase));
				toReturn.Parameters.Add(MakeParameter(columnDef));
			}
			toReturn.CommandType = CommandType.Text;
			return toReturn;
		}

		private static string GetInsertSelectColumnListClause(CsvRecord headers)
		{
			var insertSelectColumnList = new StringBuilder();
			for (int index = 0; index < headers.Fields.Length; index++)
			{
				var header = headers.Fields[index];
				if (index > 0)
					insertSelectColumnList.Append(", ");
				insertSelectColumnList.Append('@');
				insertSelectColumnList.Append(header.Value.ToLower());
			}
			return insertSelectColumnList.ToString();
		}

		private static string GetInsertColumnListClause(CsvRecord headers)
		{
			var insertColumnList = new StringBuilder();
			for (int index = 0; index < headers.Fields.Length; index++)
			{
				var header = headers.Fields[index];
				if (index > 0)
					insertColumnList.Append(", ");
				insertColumnList.Append('[');
				insertColumnList.Append(header.Value);
				insertColumnList.Append(']');
			}
			return insertColumnList.ToString();
		}

		private static string GetNonPkSetClause(IEnumerable<ColumnDef> columnDefs, CsvRecord headers)
		{
			var setClause = new StringBuilder();
			List<ColumnDef> nonPrimaryKeyColumns =
				columnDefs
					.Where
					(
						cd =>
						!cd.IsInPrimaryKey
						&& headers.Fields.Any
						   	(
						   		f => f.Value.ToLower() == cd.ColumnName.ToLower()
						   	)
					)
					.ToList();
			for (int index = 0; index < nonPrimaryKeyColumns.Count; index++)
			{
				ColumnDef columnDef = nonPrimaryKeyColumns[index];
				setClause.Append(index == 0 ? "SET " : ", ");
				setClause.Append('[');
				setClause.Append(columnDef.ColumnName);
				setClause.Append("] = @");
				setClause.Append(columnDef.ColumnName.ToLower());
			}

			return setClause.ToString();
		}

		private static string GetPkWhereClause(IEnumerable<ColumnDef> columnDefs)
		{
			var pkWhereClause = new StringBuilder();
			List<ColumnDef> pkColumns = columnDefs.Where(cd => cd.IsInPrimaryKey).ToList();
			for (int index = 0; index < pkColumns.Count; index++)
			{
				ColumnDef columnDef = pkColumns[index];
				pkWhereClause.Append(index == 0 ? "WHERE " : " AND ");
				pkWhereClause.Append('[');
				pkWhereClause.Append(columnDef.ColumnName);
				pkWhereClause.Append("] = @");
				pkWhereClause.Append(columnDef.ColumnName.ToLower());
			}
			return pkWhereClause.ToString();
		}

		private static SqlParameter MakeParameter(ColumnDef columnDef)
		{
			var toReturn = new SqlParameter {ParameterName = "@" + columnDef.ColumnName.ToLower()};

			switch(columnDef.DataType)
			{
				case "nvarchar":
					toReturn.SqlDbType = SqlDbType.NVarChar;
					toReturn.Size = int.MaxValue;
					break;
				case "varchar":
					toReturn.SqlDbType = SqlDbType.VarChar;
					toReturn.Size = int.MaxValue;
					break;
				case "int":
					toReturn.SqlDbType = SqlDbType.Int;
					break;
				case "money":
					toReturn.SqlDbType = SqlDbType.Money;
					break;
				case "datetime":
					toReturn.SqlDbType = SqlDbType.DateTime;
					break;
				case "smalldatetime":
					toReturn.SqlDbType = SqlDbType.SmallDateTime;
					break;
				case "numeric":
					toReturn.SqlDbType = SqlDbType.Decimal;
					break;
				case "varbinary":
					toReturn.SqlDbType = SqlDbType.VarBinary;
					toReturn.Size = int.MaxValue;
					break;
				case "text":
					toReturn.SqlDbType = SqlDbType.Text;
					toReturn.Size = int.MaxValue;
					break;
                case "bit":
                    toReturn.SqlDbType = SqlDbType.Bit;
                    break;
				default:
					throw new NotSupportedException(columnDef.DataType + " not supported.");
			}
			return toReturn;
		}

		private static ILookup<string,ColumnDef> GetTableDefs(SqlCommand getColumnsCmd)
		{
			var all = new List<ColumnDef>();
			using (var rdr = getColumnsCmd.ExecuteReader())
			{
				while (rdr.Read())
				{
					all.Add(
						new ColumnDef
							{
								TableName = (string) rdr["TableName"],
								ColumnName = (string) rdr["ColumnName"],
								DataType = (string)rdr["DataType"],
								ColumnPosition = (int) rdr["ColumnPosition"],
								Nullable = (bool) rdr["Nullable"],
								IsIdentity = (bool) rdr["IsIdentity"],
								IsInPrimaryKey = (bool) rdr["InPK"],
							}
						);
				}
			}
			return all.ToLookup(cd => cd.TableName);
		}

		public class ColumnDef
		{
			public string TableName { get; set; }
			public string ColumnName { get; set; }
			public string DataType { get; set; }
			public int ColumnPosition { get; set; }
			public bool Nullable { get; set; }
			public bool IsIdentity { get; set; }
			public bool IsInPrimaryKey { get; set; }
		}


		private static SqlCommand CreateGetColumnsCommand(SqlConnection conn)
		{
			const string text = @"select 
t.name TableName, 
c.name ColumnName, 
[types].name DataType,
c.is_nullable Nullable,
c.column_id ColumnPosition, 
c.is_identity IsIdentity, 
convert(bit,case when pks.column_id is not null then 1 else 0 end) as InPK
from
(
	sys.columns c 
	inner join sys.tables t on t.object_id = c.object_id
	inner join sys.types [types] on c.system_type_id = types.system_type_id and c.user_type_id = types.user_type_id
)
left join 
(
	select i.object_id, i.name, ic.column_id 
	from sys.index_columns ic inner join sys.indexes i on ic.index_id = i.index_id and ic.object_id = i.object_id
	where is_primary_key = 1
) pks on t.object_id = pks.object_id and c.column_id = pks.column_id
order by t.name, c.column_id
";
			var cmd = new SqlCommand(text, conn) {CommandType = CommandType.Text};
			return cmd;
		}
	}
}
