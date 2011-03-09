using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LinqToCsv
{
	public static class MyCsvReader
	{
		private enum State
		{
			ExpectingFieldStart,
			ReadingUnQuotedFieldValue,
			ReadingQuotedFieldValue,
			FoundFirstQuoteInQuotedValue,
			FoundCarriageReturnAtRecordEnd,
		}

		public static IEnumerable<CsvRecord> GetCsvRecords(this Stream contents, Encoding encoding)
		{
			var currentState = State.ExpectingFieldStart;
			var currentFieldValue = new StringBuilder(500);
			var currentRecordFields = new List<CsvField>();
			int currentFieldIndex = 0;
			int currentRecordLineIndex = 0;
			using (var rdr = new StreamReader(contents, encoding))
			{
				while (!rdr.EndOfStream)
				{
					var buffer = new char[1024*32];
					int charactersRead = rdr.ReadBlock(buffer, 0, buffer.Length);

					for (int i = 0; i < buffer.Length && i < charactersRead; i++)
					{
						char currentChar = buffer[i];
						switch (currentState)
						{
							case State.ExpectingFieldStart:
								{
									#region ExpectingFieldStart
									switch (currentChar)
									{
										case '"':
											//we just started a quoted field value.
											currentState = State.ReadingQuotedFieldValue;
											break;
										case ',':
											//we just encountered the end of the field (and it's empty).
											currentRecordFields.Add(new CsvField {Index = currentFieldIndex, Value = currentFieldValue.ToString()});
											currentFieldValue.Clear();
											currentFieldIndex++;
											currentState = State.ExpectingFieldStart;
											break;
										case '\r':
											// we just encountered the end of the record
											currentRecordFields.Add(new CsvField {Index = currentFieldIndex, Value = currentFieldValue.ToString()});
											currentFieldValue.Clear();
											yield return new CsvRecord {Fields = currentRecordFields.ToArray(), FileLineNumber = currentRecordLineIndex};
											currentRecordFields.Clear();
											currentFieldIndex = 0;
											currentRecordLineIndex++;
											currentState = State.FoundCarriageReturnAtRecordEnd;
											break;
										case '\n':
											// we just encountered the end of the record
											currentRecordFields.Add(new CsvField {Index = currentFieldIndex, Value = currentFieldValue.ToString()});
											currentFieldValue.Clear();
											yield return new CsvRecord {Fields = currentRecordFields.ToArray(), FileLineNumber = currentRecordLineIndex};
											currentRecordFields.Clear();
											currentFieldIndex = 0;
											currentRecordLineIndex++;
											currentState = State.ExpectingFieldStart;
											break;
										default:
											// we just read the first character of a non-quoted field value.
											currentFieldValue.Append(currentChar);
											currentState = State.ReadingUnQuotedFieldValue;
											break;
									}
									#endregion
									continue;
								}
							case State.ReadingUnQuotedFieldValue:
								{
									#region ReadingUnQuotedFieldValue
									switch (currentChar)
									{
										case ',':
											//we just encountered the end of the field.
											currentRecordFields.Add(new CsvField {Index = currentFieldIndex, Value = currentFieldValue.ToString()});
											currentFieldValue.Clear();
											currentFieldIndex++;
											currentState = State.ExpectingFieldStart;
											break;
										case '\r':
											// we just encountered the end of the record
											currentRecordFields.Add(new CsvField {Index = currentFieldIndex, Value = currentFieldValue.ToString()});
											currentFieldValue.Clear();
											yield return new CsvRecord { Fields = currentRecordFields.ToArray(), FileLineNumber = currentRecordLineIndex };
											currentRecordFields.Clear();
											currentFieldIndex = 0;
											currentRecordLineIndex++;
											currentState = State.FoundCarriageReturnAtRecordEnd;
											break;
										case '\n':
											// we just encountered the end of the record
											currentRecordFields.Add(new CsvField {Index = currentFieldIndex, Value = currentFieldValue.ToString()});
											currentFieldValue.Clear();
											yield return new CsvRecord { Fields = currentRecordFields.ToArray(), FileLineNumber = currentRecordLineIndex };
											currentRecordFields.Clear();
											currentFieldIndex = 0;
											currentRecordLineIndex++;
											currentState = State.ExpectingFieldStart;
											break;
										default:
											currentFieldValue.Append(currentChar);
											break;
									}
									#endregion
									continue;
								}
							case State.ReadingQuotedFieldValue:
								{
									#region ReadingQuotedFieldValue
									switch (currentChar)
									{
										case '"':
											// this is either the end of the field value or it's the beginning of an escaped quote character
											currentState = State.FoundFirstQuoteInQuotedValue;
											break;
										//TODO: if we read a carriage return or newline, we need to increment currentRecordLineIndex
										default:
											currentFieldValue.Append(currentChar);
											break;
									}
									#endregion
									continue;
								}
							case State.FoundFirstQuoteInQuotedValue:
								{
									#region FoundFirstQuoteInQuotedValue
									switch (currentChar)
									{
										case '"':
											// we found the second quote, so we'll remember a quote in the field value and continue reading a quoted field value.
											currentFieldValue.Append(currentChar);
											currentState = State.ReadingQuotedFieldValue;
											break;
										case ',':
											//we just encountered the end of the field.
											currentRecordFields.Add(new CsvField { Index = currentFieldIndex, Value = currentFieldValue.ToString() });
											currentFieldValue.Clear();
											currentFieldIndex++;
											currentState = State.ExpectingFieldStart;
											break;
										case '\r':
											// we just encountered the end of the record
											currentRecordFields.Add(new CsvField {Index = currentFieldIndex, Value = currentFieldValue.ToString()});
											currentFieldValue.Clear();
											yield return new CsvRecord { Fields = currentRecordFields.ToArray(), FileLineNumber = currentRecordLineIndex };
											currentRecordFields.Clear();
											currentFieldIndex = 0;
											currentRecordLineIndex++;
											currentState = State.FoundCarriageReturnAtRecordEnd;
											break;
										case '\n':
											// we just encountered the end of the record
											currentRecordFields.Add(new CsvField {Index = currentFieldIndex, Value = currentFieldValue.ToString()});
											currentFieldValue.Clear();
											yield return new CsvRecord { Fields = currentRecordFields.ToArray(), FileLineNumber = currentRecordLineIndex };
											currentRecordFields.Clear();
											currentFieldIndex = 0;
											currentRecordLineIndex++;
											currentState = State.ExpectingFieldStart;
											break;
										default:
											//TODO: report the source line number and stuff.
											throw new InvalidOperationException();
									}
									#endregion
									continue;
								}
							case State.FoundCarriageReturnAtRecordEnd:
								{
									#region FoundCarriageReturnAtRecordEnd
									switch (currentChar)
									{
										case '\n':
											currentState = State.ExpectingFieldStart;
											break;
										case '"':
											//we just started a quoted field value.
											currentState = State.ReadingQuotedFieldValue;
											break;
										case ',':
											//we just encountered the end of the field (and it's empty).
											currentRecordFields.Add(new CsvField { Index = currentFieldIndex, Value = currentFieldValue.ToString() });
											currentFieldValue.Clear();
											currentFieldIndex++;
											currentState = State.ExpectingFieldStart;
											break;
										case '\r':
											// we just encountered the end of the record
											currentRecordFields.Add(new CsvField {Index = currentFieldIndex, Value = currentFieldValue.ToString()});
											currentFieldValue.Clear();
											yield return new CsvRecord { Fields = currentRecordFields.ToArray(), FileLineNumber = currentRecordLineIndex };
											currentRecordFields.Clear();
											currentFieldIndex = 0;
											currentRecordLineIndex++;
											currentState = State.FoundCarriageReturnAtRecordEnd;
											break;
										default:
											// we just read the first character of a non-quoted field value.
											currentFieldValue.Append(currentChar);
											currentState = State.ReadingUnQuotedFieldValue;
											break;
									}
									#endregion
									continue;
								}
							default:
								throw new InvalidOperationException();
						}
					}
				}
				// if we have something in a current field
				if (currentFieldValue.Length > 0)
				{
					currentRecordFields.Add(new CsvField {Index = currentFieldIndex, Value = currentFieldValue.ToString()});
					currentFieldValue.Clear();
					yield return new CsvRecord {Fields = currentRecordFields.ToArray(), FileLineNumber = currentRecordLineIndex};
				}

				//TODO: At this point if we are not in State.ExpectingFieldStart, then the last field value might be incomplete.
			}
		}

		public static IEnumerable<CsvRecord> GetCsvRecords(this Stream contents)
		{
			return GetCsvRecords(contents, Encoding.GetEncoding(1252));
		}

	}


	public class CsvRecord
	{
		public int FileLineNumber { get; set; }
		public CsvField[] Fields { get; set; }

		public string this[int index]
		{
			get { return Fields[index].Value; }
		}
	}

	public struct CsvField
	{
		public int Index { get; set; }
		public string Value { get; set; }
	}

}
