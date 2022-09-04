/*
* Copyright (c) 2005 Poderosa Project, All Rights Reserved.
* $Id: MacroInterface.cs,v 1.2 2005/04/20 08:45:45 okajima Exp $
*/
using System;
using System.Collections;

using Poderosa.ConnectionParam;


namespace Poderosa.Macro
{
    /// <summary>
    /// <en>This class is the root of the macro functionality.</en>
    /// </summary>
    /// <remarks>
    /// <en>Use properties and methods after the macro creates an instance of this class. </en>
    /// </remarks>
    public sealed class Environment : MarshalByRefObject
    {

        /// <summary>
        /// <en>Gets the <see cref="ConnectionList"/> object.</en>
        /// </summary>
        public ConnectionList Connections => _connectionList;

        /// <summary>
        /// <en>Gets the <see cref="Util"/> object.</en>
        /// </summary>
        public Util Util => _util;

        /// <summary>
        /// <en>Gets the <see cref="Frame"/> object.</en>
        /// </summary>
        public Frame Frame => _frame;

        /// <summary>
        /// <en>Gets the <see cref="DebugService"/> object for debugging the macro.</en>
        /// </summary>
        public DebugService Debug => _debugService;

        private static ConnectionList _connectionList;
        private static Util _util;
        private static DebugService _debugService;
        private static Frame _frame;

        internal static void Init(ConnectionList cl, Util ui, Frame fr, DebugService ds)
        {
            _connectionList = cl;
            _util = ui;
            _frame = fr;
            _debugService = ds;
        }
    }

    /// <summary>
    /// <en>A collection of <see cref="Connection"/> objects.</en>
    /// </summary>
    public abstract class ConnectionList : MarshalByRefObject, IEnumerable
    {
        /// <summary>
        /// <en>Enumerates each <see cref="Connection"/> objects.</en>
        /// </summary>
        public abstract IEnumerator GetEnumerator();

        /// <summary>
        /// <en>Returns the active connection of Poderosa.</en>
        /// <en>If there are no active connections, returns null.</en>
        /// </summary>
        public abstract Connection ActiveConnection { get; }

        /// <summary>
        /// <en>Opens a new connection.</en>
        /// </summary>
        /// <remarks>
        /// <en>If the connection fails, Poderosa shows an error message box and returns null to the macro.</en>
        /// </remarks>
        /// <seealso cref="TerminalParam"/>
        /// <seealso cref="TCPTerminalParam"/>
        /// <seealso cref="TelnetTerminalParam"/>
        /// <param name="param"><en>The <see cref="TerminalParam"/> object that contains parameters for the connection.</en></param>
        /// <returns><en>A <see cref="Connection"/> object that describes the new connection.</en></returns>
        public abstract Connection Open(TerminalParam param);
    }


    /// <summary>
    /// <en>Describes a connection.</en>
    /// </summary>
    public abstract class Connection : MarshalByRefObject
    {
        /// <summary>
        /// <en>Receives a line from the connection.</en>
        /// </summary>
        /// <remarks>
        /// <para>
        /// <en> When no data is available or the new line characters are not sent, the execution of this method is blocked.</en></para>
        /// <para>
        /// <en> Especially note that this method could not be used to wait a prompt string since it does not contain new line characters. To wait a prompt, use the <see cref="ReceiveData"/> method instead of ReceiveLine method.</en>
        /// </para>
        /// <para>
        /// <en> Additionally, CR and NUL are ignored in the data from the host.</en></para>
        /// <seealso cref="ReceiveData"/>
        /// </remarks>
        /// <example>
        /// <code>
        /// import Poderosa.Macro;
        /// import System.IO;
        /// var env = new Environment();
        /// var output = new StreamWriter("...
        /// var connection = env.Connections.ActiveConnection;
        /// var line = connection.ReceiveLine();
        /// while(line!="end") { //wait for "end"
        ///   output.WriteLine(line);
        ///   line = connection.ReceiveLine();
        /// }
        /// output.Close();
        /// </code>
        /// </example>
        /// <returns><en>The received line without new line characters.</en></returns>
        public abstract string ReceiveLine();

        /// <summary>
        /// <en>Receives data from the connection.</en>
        /// </summary>
        /// <remarks>
        /// <para>
        /// <en>  When no data is available, the execution of this method is blocked.</en>
        /// </para>
        /// <para>
        /// <en> This method returns the whole data from the previous call of the ReceiveData method. Though this method can obtain the data even if it does not contain new line characters, the split into lines is responsible for the macro. Please compare to the <see cref="ReceiveLine"/> method.</en>
        /// </para>
        /// <para>
        /// <en> CR and NUL are ignored in the data from the host. The line breaks are determined by LF.</en>
        /// </para>
        /// <seealso cref="ReceiveLine"/>
        /// </remarks>
        /// <example>
        /// <code>
        /// import Poderosa.Macro;
        /// import System.IO;
        /// var env = new Environment();
        /// var connection = env.Connections.ActiveConnection;
        /// var data = connection.ReceiveData();
        /// if(data.EndsWith("login: ") {
        ///	  ...
        /// </code>
        /// </example>
        /// <returns><en>The received data.</en></returns>
        public abstract string ReceiveData();

        /// <summary>
        /// <en>Writes a comment to the log. If the connection is not set to record the log, this method does nothing.</en>
        /// </summary>
        /// <example>
        /// <code>
        /// import Poderosa.Macro;
        /// var env = new Environment();
        /// var connection = env.Connections.ActiveConnection;
        /// connection.WriteComment("starting macro...");
        /// </code>
        /// </example>
        /// <param name="comment"><en>The comment string</en></param>
        public abstract void WriteComment(string comment);
    }

    /// <summary>
    /// <en>Implements the appearances of the application.</en>
    /// </summary>
    public abstract class Frame : MarshalByRefObject
    {
    }


    /// <summary>
    /// <en>Implements several utility functions for macros.</en>
    /// </summary>
    public abstract class Util : MarshalByRefObject
    {
    }

    /// <summary>
    /// <en>Implements features for testing and debugging the macro.</en>
    /// </summary>
    /// <remarks>
    /// <para>
    /// <en> The macro trace window is displayed when the "shows trace window" option is checked in the dialog box of the macro property.</en>
    /// </para>
    /// </remarks>
    public abstract class DebugService : MarshalByRefObject
    {
    }

}
