using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLog;
using TaleWorlds.Localization;

namespace Coop.GameInterface.Serializers
{
    [Serializable]
    public class TextObjectSerializer : ICustomSerializer
    {
        static ConcurrentStack<object> _stack = new ConcurrentStack<object>();
        string _text;
        Dictionary<string, object> _attributes = new Dictionary<string, object>();
        public TextObjectSerializer(TextObject textObject)
        {
            if (_stack.Contains(textObject))
            {
                return;
            }
            else
            {
                _stack.Push(textObject);
            } 

            _text = (string)textObject.GetType()
                .GetField("Value", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(textObject);

            if (textObject.Attributes != null)
            {
                foreach (KeyValuePair<string, object> pair in textObject.Attributes)
                {
                    switch (pair.Value)
                    {
                        case TextObject textObj:
                            _attributes.Add(pair.Key, new TextObjectSerializer(textObj));
                            break;
                        case string str:
                            _attributes.Add(pair.Key, str);
                            break;
                        case int i:
                            _attributes.Add(pair.Key, i);
                            break;
                        case float f:
                            _attributes.Add(pair.Key, f);
                            break;
                        case null:
                            _attributes.Add(pair.Key, null);
                            break;
                        default:
                            string warningMessage = $"Unknown attribute type {pair.Value.GetType()}";
                            LogManager.GetCurrentClassLogger().Warn(warningMessage);
                            break;
                    }
                }
            }

            object _;
            _stack.TryPop(out _);
        }

        public object Deserialize()
        {
            TextObject textObject = new TextObject();
            textObject.GetType()
                .GetField("Value", BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(textObject, _text);

            textObject.CacheTokens();

            foreach(var attribute in _attributes)
            {
                if(attribute.Value is TextObjectSerializer textObjectSerializer)
                {
                    textObject.SetTextVariable(attribute.Key, (TextObject)textObjectSerializer.Deserialize());
                }
                else if(attribute.Value is string str)
                {
                    textObject.SetTextVariable(attribute.Key, str);
                }
                else if (attribute.Value is int i)
                {
                    textObject.SetTextVariable(attribute.Key, i);
                }
                else if (attribute.Value is float f)
                {
                    textObject.SetTextVariable(attribute.Key, f);
                }
                else if (attribute.Value is null)
                {
                    textObject.SetTextVariable(attribute.Key, (string)null);
                }
                else
                {
                    string warningMessage = $"Unknown attribute type {attribute.Value.GetType()}";
                    LogManager.GetCurrentClassLogger().Warn(warningMessage);
                }
                
            }
            

            return textObject;
        }

        public void ResolveReferenceGuids()
        {
            // Do nothing no references exist
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
}