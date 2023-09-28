using UnityEngine;

namespace LO {

    public class LOLogger {

        /// <summary>
        /// Debug log while DEBUG_MODE
        /// </summary>
        /// <param name="log"></param>
        public static void DI(string log) {
#if DEBUG_MODE
            I(log);
#endif
        }

        /// <summary>
        /// Debug log with prefix while DEBUG_MODE
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="log"></param>
        public static void DI(string prefix, string log) {
#if DEBUG_MODE
            I(prefix, log);
#endif
        }

        /// <summary>
        /// Log message
        /// </summary>
        /// <param name="log">log</param>
        public static void I(string log) {
            Debug.Log(log);
        }

        /// <summary>
        /// Log message with prefix
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="log"></param>
        public static void I(string prefix, string log) {
            I($"<b>[{prefix}]</b> : {log}");
        }

        /// <summary>
        /// Log error message
        /// </summary>
        /// <param name="errLog"></param>
        public static void E(string errLog) {
            Debug.LogError(errLog);
        }

        /// <summary>
        /// Sent to google analysis
        /// </summary>
        /// <param name="eventName">event name</param>
        public static void A(string eventName) {

            // TODO : ga implement
        }
    }
}