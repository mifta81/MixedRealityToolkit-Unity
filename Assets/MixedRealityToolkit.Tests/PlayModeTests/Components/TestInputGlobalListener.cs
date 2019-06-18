﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.MixedReality.Toolkit.Input;

namespace Microsoft.MixedReality.Toolkit.Tests
{
    internal class TestInputGlobalListener: InputSystemGlobalListener, IMixedRealityPointerHandler, IMixedRealitySpeechHandler
    {
        // Parameters, which are set by child classes
        protected bool useObjectBasedRegistration = false;
        protected bool registerSpeechOnly = false;

        // Values changed by class to validate event receiving
        public int pointerDownCount = 0;
        public int pointerDraggedCount = 0;
        public int pointerUpCount = 0;
        public int pointerClickedCount = 0;
        public int speechCount = 0;

        protected override void OnEnable()
        {
            pointerDownCount = 0;
            pointerDraggedCount = 0;
            pointerUpCount = 0;
            pointerClickedCount = 0;
            speechCount = 0;

            if (useObjectBasedRegistration)
            {
                base.OnEnable();
            }
            else if (InputSystem != null)
            {
                if (registerSpeechOnly)
                {
                    InputSystem.RegisterHandler<IMixedRealitySpeechHandler>(this);
                }
                else
                {
                    InputSystem.RegisterHandler<IMixedRealitySpeechHandler>(this);
                    InputSystem.RegisterHandler<IMixedRealityPointerHandler>(this);
                }
            }
        }

        protected override void Start()
        {
            if (useObjectBasedRegistration)
            {
                base.Start();
            }
        }

        protected override void OnDisable()
        {
            if (useObjectBasedRegistration)
            {
                base.OnDisable();
            }
            else if(InputSystem != null)
            {
                if (registerSpeechOnly)
                {
                    InputSystem.UnregisterHandler<IMixedRealitySpeechHandler>(this);
                }
                else
                {
                    InputSystem.UnregisterHandler<IMixedRealitySpeechHandler>(this);
                    InputSystem.UnregisterHandler<IMixedRealityPointerHandler>(this);
                }
            }
        }

        public void OnPointerDown(MixedRealityPointerEventData eventData)
        {
            pointerDownCount++;
        }

        public void OnPointerDragged(MixedRealityPointerEventData eventData)
        {
            pointerDraggedCount++;
        }

        public void OnPointerUp(MixedRealityPointerEventData eventData)
        {
            pointerUpCount++;
        }

        public void OnPointerClicked(MixedRealityPointerEventData eventData)
        {
            pointerClickedCount++;
        }

        public void OnSpeechKeywordRecognized(SpeechEventData eventData)
        {
            speechCount++;
        }
    }

    internal class TestInputGlobalListenerObjectBased : TestInputGlobalListener
    {
        TestInputGlobalListenerObjectBased()
        {
            useObjectBasedRegistration = true;
            registerSpeechOnly = false;
        }
    }

    internal class TestInputGlobalListenerHandlerBasedAllHandlers : TestInputGlobalListener
    {
        TestInputGlobalListenerHandlerBasedAllHandlers()
        {
            useObjectBasedRegistration = false;
            registerSpeechOnly = false;
        }
    }

    internal class TestInputGlobalListenerHandlerBasedSpeechHandler : TestInputGlobalListener
    {
        TestInputGlobalListenerHandlerBasedSpeechHandler()
        {
            useObjectBasedRegistration = false;
            registerSpeechOnly = true;
        }
    }
}