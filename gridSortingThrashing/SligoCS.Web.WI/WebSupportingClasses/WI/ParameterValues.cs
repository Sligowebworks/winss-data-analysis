using System;
using System.Collections.Generic;

namespace SligoCS.Web.WI.WebSupportingClasses.WI
{
    public abstract class ParameterValues
    {
        private string _value;

        protected ParameterValues()
        {
            Value = QueryStringUtils.InitializeProperty(this.GetType().Name).ToString();
        }

        public abstract SerializableDictionary<String, String> Range
        {
            get;
        }
        /// <summary>
        /// returns the right side of the association
        /// </summary>
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                try 
                {
                    validateValue(value);
                    _value = value; 
                } catch (Exception e)
                {
                     throw new Exception("Invalid Value Assigned for:[" + this.GetType().Name + "]",  e);
                }
            }
        }
        /// <summary>
        /// returns the left side of the association
        /// </summary>
        public string Key
        {
            get
            {
                return getKey(Value);
            }
        }
        /// <summary>
        /// Looks up the key for the provided value, not the internal Value of the object
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string getKey(String value)
        {
            string key = String.Empty;
            try { validateValue(value); }
            catch (Exception e) { throw new Exception("getKey() value did not validate", e); }

            foreach (KeyValuePair<string, string> item in Range)
            {
                if (item.Value.Equals(Value)) key = item.Key;
            }
            return key;
        }

        public void validateValue(String value)
        {
            if (!Range.ContainsValue(value)) throw new Exception("Out of Range exception:[" + value + "]");
        }

        public static implicit operator string(ParameterValues o )
        {
            return o.Value;
        }
        
        public override string  ToString()
        {
            return this.Value;
        }

        /// <summary>
        /// intended for use in QueryString
        /// Property represents: &paramName=
        /// has no meaning in abstract class
        /// </summary>
        /// <returns></returns>
        public virtual string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        /// <summary>
        /// Compares the Key of the current Value to the Key Provided
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bool</returns>
        public bool CompareToKey(String key)
        {
            if (key == Key) return true;
            
            return false;
        }
    }
}
