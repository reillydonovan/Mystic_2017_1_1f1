// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace HoloToolkit.Unity
{
    /// <summary>
    /// The purpose of this class is to provide a cached reference to the main camera. Calling Camera.main
    /// executes a FindByTag on the scene, which will get worse and worse with more tagged objects.
    /// </summary>
    public static class ObjectCache
    { /*
        private static GameObject cachedObject;

        /// <summary>
        /// Returns a cached reference to the main camera and uses Camera.main if it hasn't been cached yet.
        /// </summary>
        public static GameObject RemoteObject
        {
            get
            {
               if (cachedObject == null)
                {
                    return Refresh(RemoteObject.main);
                }
                return cachedObject;
            }
        }

        /// <summary>
        /// Set the cached camera to a new reference and return it
        /// </summary>
        /// <param name="newMain">New main camera to cache</param>
        public static GameObject Refresh(GameObject newMain)
        {
            return cachedObject = newMain;
        }
        */
    }
    
}
