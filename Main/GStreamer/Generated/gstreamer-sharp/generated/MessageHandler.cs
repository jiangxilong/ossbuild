// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gst {

	using System;

	public delegate void MessageHandler(object o, MessageArgs args);

	public class MessageArgs : Gst.GLib.SignalArgs {
		public Gst.Message Message{
			get {
				return (Gst.Message) Args [0];
			}
		}

	}
}
