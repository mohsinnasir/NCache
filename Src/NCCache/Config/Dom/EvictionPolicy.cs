// Copyright (c) 2018 Alachisoft
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections;
using System.Text;
using Alachisoft.NCache.Common.Configuration;
using Alachisoft.NCache.Runtime.Serialization;
using Runtime = Alachisoft.NCache.Runtime;

namespace Alachisoft.NCache.Config.Dom
{
    [Serializable]
    public class EvictionPolicy: ICloneable,ICompactSerializable
    {
        bool enabled;
        string defaultPriority;
        decimal evictionRatio;
        string policy;
        public EvictionPolicy() { }

        [ConfigurationAttribute("enabled-eviction")]//Changes for New Dom from enabled
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        [ConfigurationAttribute("default-priority")]
        public string DefaultPriority
        {
            get { return defaultPriority; }
            set { defaultPriority = value; }
        }

        [ConfigurationAttribute("policy")]
        public string Policy
        {
            get { return policy; }
            set { policy = value; }
        }


        [ConfigurationAttribute("eviction-ratio","%")]
        public decimal EvictionRatio
        {
            get { return evictionRatio; }
            set { evictionRatio = value; }
        }

        #region ICloneable Members

        public object Clone()
        {
            EvictionPolicy policy = new EvictionPolicy();
            policy.Enabled = Enabled;
            policy.DefaultPriority = DefaultPriority != null ? (string)DefaultPriority.Clone(): null;
            policy.EvictionRatio = EvictionRatio;
            policy.Policy = Policy ;
            return policy;
        }

        #endregion

        #region ICompactSerializable Members

        public void Deserialize(Runtime.Serialization.IO.CompactReader reader)
        {
            enabled = reader.ReadBoolean();
            defaultPriority = reader.ReadObject() as string;
            object obj = reader.ReadObject();
            if(obj != null)
                evictionRatio = (decimal)obj;
            policy = reader.ReadObject() as string;
        }

        public void Serialize(Runtime.Serialization.IO.CompactWriter writer)
        {            
            writer.Write(enabled);
            writer.WriteObject(defaultPriority);
            writer.WriteObject(evictionRatio);
            writer.WriteObject(policy);            
        }

        #endregion
    }
}