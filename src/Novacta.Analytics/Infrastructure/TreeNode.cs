// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Novacta.Analytics.Infrastructure
{
    /// <summary>
    /// Represents a node in a tree having the specified data.
    /// </summary>
    /// <typeparam name="T">The type of the node data.</typeparam>
    /// <seealso href="https://en.wikipedia.org/wiki/Tree_%28data_structure%29"/>
    internal class TreeNode<T> 
    {

        #region State

        private readonly List<TreeNode<T>> children =
            new();

        /// <summary>
        /// Gets the data of this <see cref="TreeNode{T}"/>.
        /// </summary>
        /// <value>The data of this instance.</value>
        public T Data { get; }

        /// <summary>
        /// Gets the parent <see cref="TreeNode{T}"/> of this node.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If this <see cref="TreeNode{T}"/> has no parent node, 
        /// this property returns <b>null</b>.
        /// </para>
        /// </remarks>
        /// <value>The parent of this instance.</value>
        public TreeNode<T> Parent { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="TreeNode{T}"/> is a 
        /// root node, i.e. it
        /// has no parent node.
        /// </summary>
        /// <value><c>true</c> if this instance is a root node; 
        /// otherwise, <c>false</c>.</value>
        public bool IsRoot { get { return this.Parent == null; } }

        /// <summary>
        /// Gets the level of this <see cref="TreeNode{T}"/>.
        /// </summary>
        /// <value>The level of this <see cref="TreeNode{T}"/>.</value>
        /// <remarks>
        /// <para>
        /// The level of a root node is zero, that of any further node 
        /// is defined by 1 + the number of connections between 
        /// that node and its root.
        /// </para>
        /// </remarks>
        public int Level
        {
            get
            {
                if (this.IsRoot) {
                    return 0;
                }
                return this.Parent.Level + 1;
            }
        }

        /// <summary>
        /// Gets the degree of this <see cref="TreeNode{T}"/>.
        /// </summary>
        /// <value>The degree of this <see cref="TreeNode{T}"/>.</value>
        /// <remarks>
        /// <para>
        /// The degree of a node is the number of its sub trees.
        /// </para>
        /// </remarks>
        public int Degree
        {
            get
            {
                return this.children.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="TreeNode{T}"/> 
        /// is terminal.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A node is terminal if and only if the number of its sub trees 
        /// is zero.
        /// </para>
        /// </remarks>
        /// <value><c>true</c> if this <see cref="TreeNode{T}"/> is terminal; 
        /// otherwise, <c>false</c>.</value>
        public bool IsTerminal { get { return this.Degree == 0; } }

        /// <summary>
        /// Gets a value indicating whether 
        /// this <see cref="TreeNode{T}"/> is branch.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A node is branch if and only if the number of its sub trees 
        /// is nonzero.
        /// </para>
        /// </remarks>
        /// <value><c>true</c> if this <see cref="TreeNode{T}"/> is terminal; 
        /// otherwise, <c>false</c>.</value>
        public bool IsBranch { get { return !this.IsTerminal; } }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents 
        /// this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "Level: {0}. Degree: {1}. Data: {2}",
                this.Level, 
                this.Degree, 
                this.Data);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode{T}"/> class
        /// having the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        public TreeNode(T data)
        {
            this.Data = data;
        }

        #endregion

        #region Children

        /// <summary>
        /// Adds a child to this <see cref="TreeNode{T}" />.
        /// </summary>
        /// <param name="data">The data of the added child.</param>
        /// <returns>The added node.</returns>
        public TreeNode<T> AddChild(T data)
        {
            var child = new TreeNode<T>(data)
            {
                Parent = this
            };
            this.children.Add(child);
            return child;
        }

        /// <summary>
        /// Removes the specified child from this <see cref="TreeNode{T}" />.
        /// </summary>
        /// <param name="node">The node to be removed.</param>
        /// <returns><c>true</c> if the <paramref name="node"/> is successfully 
        /// removed, <c>false</c> otherwise.</returns>
        public bool RemoveChild(TreeNode<T> node)
        {
            return this.children.Remove(node);
        }

        /// <summary>
        /// Gets the children of this <see cref="TreeNode{T}"/>.
        /// </summary>
        /// <value>The children of this <see cref="TreeNode{T}"/>.</value>
        public ReadOnlyCollection<TreeNode<T>> Children
        {
            get { return this.children.AsReadOnly(); }
        }

        #endregion

        #region Ancestors

        /// <summary>
        /// Gets the ancestors of this <see cref="TreeNode{T}"/>.
        /// </summary>
        /// <value>The enumeration of the ancestors of 
        /// this <see cref="TreeNode{T}"/>.</value>
        public IEnumerable<TreeNode<T>> Ancestors
        {
            get
            {
                return this.SelfAndAncestors.Skip(1);
            }
        }

        /// <summary>
        /// Gets this <see cref="TreeNode{T}"/> and its ancestors.
        /// </summary>
        /// <value>The enumeration of this <see cref="TreeNode{T}"/> 
        /// and its ancestors.</value>
        public IEnumerable<TreeNode<T>> SelfAndAncestors
        {
            get
            {
                TreeNode<T> current = this;
                while (current != null) {
                    yield return current;
                    current = current.Parent;
                }
            }
        }

        #endregion

        #region Descendants

        /// <summary>
        /// Gets the descendants of this <see cref="TreeNode{T}"/>.
        /// </summary>
        /// <value>The enumeration of the descendants of 
        /// this <see cref="TreeNode{T}"/>.</value>
        public IEnumerable<TreeNode<T>> Descendants
        {
            get
            {
                return this.SelfAndDescendants.Skip(1);
            }
        }

        /// <summary>
        /// Gets this <see cref="TreeNode{T}"/> and its descendants.
        /// </summary>
        /// <value>The enumeration of this <see cref="TreeNode{T}"/> 
        /// and its descendants.</value>
        public IEnumerable<TreeNode<T>> SelfAndDescendants
        {
            get
            {
                yield return this;
                foreach (var child in this.Children) {
                    foreach (var item in child.SelfAndDescendants) {
                        yield return item;
                    }
                }
            }
        }

        #endregion
    }
}