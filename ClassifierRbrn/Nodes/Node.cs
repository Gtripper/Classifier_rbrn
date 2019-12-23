using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Classifier
{
    public interface INode
    {
        string Code { get; }
        string Type { get; }
        string Kind { get; }

        string SimpleDescription { get; }
        string Description { get; }

        string GetSearchResult(string input);
        bool Equals(INode node);
        bool Equals(string node);
    }

    /// <summary>
    /// 
    /// </summary>
    public class Node : INode
    {
        public string Code { get; }
        public string Type { get; }
        public string Kind { get; }
        public string SimpleDescription { get; }
        public string Description { get; }        
        private INodeRegExp regExp;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <param name="kind"></param>
        /// <param name="simpleDescription"></param>
        /// <param name="description"></param>
        /// <param name="reg"></param>
        public Node(string code, string type, string kind, string simpleDescription, string description, INodeRegExp reg)
        {
            Code = code;
            Type = type;
            Kind = kind;
            Description = description;
            SimpleDescription = simpleDescription;
            regExp = reg;
        }

        public string GetSearchResult(string input)
        {
            return regExp.GetSearchResult(input);
        }

        public bool Equals(INode node)
        {
            Contract.Requires(node != null);
            return Code.Equals(node.Code, StringComparison.InvariantCulture);
        }

        public bool Equals(string node)
        {
            return Code.Equals(node, StringComparison.InvariantCulture);
        }
    }

}
