﻿/**
 * Owned By: Ashish Kumar Gupta
 * Created By: Ashish Kumar Gupta
 * Date Created: 10/11/2021
 * Date Modified: 10/11/2021
**/

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Whiteboard
{
    /// <summary>
    /// Client-side state management for Whiteboard.
    /// Non-extendable class having functionalities to maintain state at client side. 
    /// </summary>
    public sealed class ClientBoardStateManager : IClientBoardStateManager, IClientBoardStateManagerInternal, IServerUpdateListener
    {
        // Attribute holding the single instance of this class. 
        private static ClientBoardStateManager s_instance = null;

        // Attribute holding current user id
        private string _currentUserId;

        // Clients subscribed to state manager
        private Dictionary<string, IClientBoardStateListener> _clients;

        // no. of checkpoints stored on the server
        private int _checkpointsNumber;

        // no. of states till the client can undo
        private readonly int _undoRedoCapacity = 7;

        // instance of clientBoardCommunicator
        private IClientBoardCommunicator _clientBoardCommunicator;

        // data structures to maintain state
        private Dictionary<string, BoardShape> _mapIdToBoardShape;
        private Dictionary<string, QueueElement> _mapIdToQueueElement;
        private BoardPriorityQueue _priorityQueue;

        // data structures required for undo-redo
        private BoardStack _undoStack;
        private BoardStack _redoStack;

        /// <summary>
        /// Private constructor. 
        /// </summary>
        private ClientBoardStateManager() { }

        /// <summary>
        /// Getter for s_instance. 
        /// </summary>
        public static ClientBoardStateManager Instance
        {
            get
            {
                // Create a new instance if not yet created.
                s_instance = s_instance is null ? new ClientBoardStateManager() : s_instance;
                Trace.Indent();
                Trace.WriteLineIf(s_instance==null, "Whiteboard.ClientBoardStateManager.Instance: Creating and storing a new instance.");
                Trace.WriteLine("Whiteboard.ClientBoardStateManager.Instance: Returning the stored instance.s");
                Trace.Unindent();
                return s_instance;
            }
        }

        /// <summary>
        /// Does the redo operation for client. 
        /// </summary>
        /// <returns>List of UXShapes for UX to render.</returns>
        public List<UXShape> DoRedo()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Does the undo operation for client. 
        /// </summary>
        /// <returns>List of UXShapes for UX to render.</returns>
        public List<UXShape> DoUndo()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fetches the checkpoint from server and updates the current state. 
        /// </summary>
        /// <param name="checkpointNumber">The identifier/number of the checkpoint which needs to fetched.</param>
        /// <returns>List of UXShapes for UX to render.</returns>
        public List<UXShape> FetchCheckpoint(int checkpointNumber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fetches the BoardShape object from the map.  
        /// </summary>
        /// <param name="id">Unique identifier for a BoardShape object.</param>
        /// <returns>BoardShape object with unique id equal to id.</returns>
        public BoardShape GetBoardShape(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Provides the current user's id. 
        /// </summary>
        /// <returns>The user id of current user.</returns>
        public string GetUser()
        {
            return _currentUserId;
        }

        /// <summary>
        /// Manages state and notifies UX on receiving an update from ClientBoardCommunicator.
        /// </summary>
        /// <param name="serverUpdate">BoardServerShapes signifying the update.</param>
        public void OnMessageReceived(List<BoardServerShape> serverUpdate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates and saves checkpoint. 
        /// </summary>
        /// <returns>The number/identifier of the created checkpoint.</returns>
        public int SaveCheckpoint()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the update on a shape in the state and sends it to server for broadcast. 
        /// </summary>
        /// <param name="boardShape">The object describing shape.</param>
        /// <returns>Boolean to indicate success status of update.</returns>
        public bool SaveOperation(BoardShape boardShape)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the current user id. 
        /// </summary>
        /// <param name="userId">user Id of the current user.</param>
        public void SetUser(string userId)
        {
            _currentUserId = userId;
        }

        /// <summary>
        /// Initializes state managers attributes. 
        /// </summary>
        public void Start()
        {
            // initializing all attributes 
            _checkpointsNumber = 0;
            _clientBoardCommunicator = new ClientBoardCommunicator();
            _clients = new Dictionary<string, IClientBoardStateListener>();
            _currentUserId = null;
            _mapIdToBoardShape = new Dictionary<string, BoardShape>();
            _mapIdToQueueElement = new Dictionary<string, QueueElement>();
            _priorityQueue = new BoardPriorityQueue();
            _redoStack = new BoardStack(_undoRedoCapacity);
            _undoStack = new BoardStack(_undoRedoCapacity);

            // subscribing to ClientBoardCommunicator
            _clientBoardCommunicator.Subscribe(this);
        }

        /// <summary>
        /// Subscribes to notifications from ClientBoardStateManager to get updates.
        /// </summary>
        /// <param name="listener">The subscriber. </param>
        /// <param name="identifier">The identifier of the subscriber. </param>
        /// <returns>List of UXShapes for UX to render along with an integer which specifies number of checkpoints saved on server.</returns>
        public Tuple<List<UXShape>, int> Subscribe(IClientBoardStateListener listener, string identifier)
        {
            
            throw new NotImplementedException();
        }

        
    }
}