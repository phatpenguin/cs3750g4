using System;
using System.Collections.Generic;
using System.Linq;

namespace Controls
{
	/*
	 * The types in this file come from the Caliburn Micro project: http://caliburnmicro.codeplex.com/ with minor modifications
	 * 
	 * Copyright (c) 2010 Blue Spire Consulting, Inc.
	 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
	 * 
	 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
	 * 
	 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
	 */

	/// <summary>
	/// Enables loosely-coupled publication of and subscription to events.
	/// </summary>
	public class MessageBus : IMessageBus
	{
		private readonly List<WeakReference> mSubscribers = new List<WeakReference>();

		/// <summary>
		/// Subscribes an instance to all messages declared through implementations of <see cref="IHandle{T}"/>
		/// </summary>
		/// <param name="instance">The instance to subscribe for message publication.</param>
		public void Subscribe<T>(T instance) where T : class
		{
			lock (mSubscribers)
			{
				if (mSubscribers.Any(reference => ReferenceEquals(reference.Target, instance)))
					return;

				mSubscribers.Add(new WeakReference(instance));
			}
		}

		/// <summary>
		/// Unsubscribes the instance from all messages.
		/// </summary>
		/// <param name="instance">The instance to unsubscribe.</param>
		public void Unsubscribe<T>(T instance) where T : class
		{
			lock (mSubscribers)
			{
				var found = mSubscribers
					.FirstOrDefault(reference => ReferenceEquals(reference.Target, instance));

				if (found != null)
					mSubscribers.Remove(found);
			}
		}

		/// <summary>
		/// Publishes a message.
		/// </summary>
		/// <typeparam name="TMessage">The type of message being published.</typeparam>
		/// <param name="message">The message instance.</param>
		public void Publish<TMessage>(TMessage message)
		{
			WeakReference[] toNotify;
			lock (mSubscribers)
				toNotify = mSubscribers.ToArray();
			var dead = new List<WeakReference>();

			foreach (var reference in toNotify)
			{
				var target = reference.Target as IHandle<TMessage>;

				if (target != null)
					target.Handle(message);
				else if (!reference.IsAlive)
					dead.Add(reference);
			}
			if (dead.Count > 0)
			{
				lock (mSubscribers)
					dead.ForEach(x => mSubscribers.Remove(x));
			}
		}
	}

	/// <summary>
	/// Enables loosely-coupled publication of and subscription to events.
	/// </summary>
	public interface IMessageBus
	{
		/// <summary>
		/// Subscribes an instance to all messages declared through implementations of <see cref="IHandle{T}"/>
		/// </summary>
		/// <param name="instance">The instance to subscribe for message publication.</param>
		void Subscribe<T>(T instance) where T : class;

		/// <summary>
		/// Unsubscribes the instance from all messages.
		/// </summary>
		/// <param name="instance">The instance to unsubscribe.</param>
		void Unsubscribe<T>(T instance) where T : class;

		/// <summary>
		/// Publishes a message.
		/// </summary>
		/// <typeparam name="T">The type of message being published.</typeparam>
		/// <param name="message">The message instance.</param>
		void Publish<T>(T message);
	}

	/// <summary>
	/// Denotes a class which can handle a particular type of message.
	/// </summary>
	/// <typeparam name="TMessage">The type of message to handle.</typeparam>
	public interface IHandle<in TMessage>
	{
		/// <summary>
		/// Handles the message.
		/// </summary>
		/// <param name="message">The message.</param>
		void Handle(TMessage message);
	}
}
