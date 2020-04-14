// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using System.Linq;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test conditions 
    /// about <see cref="TreeNode{T}"/> 
    /// instances.
    /// </summary>
    static class TreeNodeTest
    {
        /// <summary>
        /// Gets the tested tree.
        /// </summary>
        /// <returns>
        /// The <see cref="TreeNode{T}"/> instance to be tested.</returns>
        public static TreeNode<string> GetTestedTree()
        {
            /*
             *                A
             *               / \
             *              /   \
             *             B     C
             *                  /|\
             *                 / | \
             *                D  E  F
             *                      |
             *                      |
             *                      G
             */
            var root = new TreeNode<string>("A");

            root.AddChild("B");
            var childC = root.AddChild("C");

            childC.AddChild("D");
            childC.AddChild("E");
            var childF = childC.AddChild("F");

            childF.AddChild("G");

            return root;
        }

        /// <summary>
        /// Provides methods to test that the
        /// <see cref="TreeNode{T}.SelfAndAncestors"/>
        /// property has
        /// been properly implemented.
        /// </summary>
        public static class SelfAndAncestors
        {
            /// <summary>
            /// Provides methods to test that the
            /// <see cref="TreeNode{T}.SelfAndAncestors"/>
            /// property has
            /// been properly implemented by testing it
            /// via a <see cref="TreeNode{T}"/> instance
            /// representing node "A" in the tree
            /// returned by <see cref="GetTestedTree"/>.
            /// </summary>
            public static void NodeA(TreeNode<string> root)
            {
                var target = (
                    from node in root.SelfAndDescendants
                    where node.Data == "A"
                    select node).FirstOrDefault();

                Assert.IsNotNull(target);

                var selfAndAncestors = target.SelfAndAncestors.ToArray();

                Assert.IsTrue(selfAndAncestors.Length == 1);

                // A

                var a = (
                    from node in selfAndAncestors
                    where node.Data == "A"
                    select node).FirstOrDefault();

                var actual = a;

                Assert.IsTrue(actual.Children.Count == 2);
                Assert.AreEqual(actual.Data, "A");
                Assert.IsTrue(actual.IsTerminal == false);
                Assert.IsTrue(actual.IsBranch == true);
                Assert.IsTrue(actual.Level == 0);
                Assert.IsTrue(actual.Degree == 2);
                Assert.IsTrue(actual.IsRoot == true);

                // B

                var b = (
                    from node in selfAndAncestors
                    where node.Data == "B"
                    select node).FirstOrDefault();

                Assert.IsNull(b);

                // C

                var c = (
                    from node in selfAndAncestors
                    where node.Data == "C"
                    select node).FirstOrDefault();

                Assert.IsNull(c);

                // D

                var d = (
                    from node in selfAndAncestors
                    where node.Data == "D"
                    select node).FirstOrDefault();

                Assert.IsNull(d);

                // E

                var e = (
                    from node in selfAndAncestors
                    where node.Data == "E"
                    select node).FirstOrDefault();

                Assert.IsNull(e);

                // F

                var f = (
                    from node in selfAndAncestors
                    where node.Data == "F"
                    select node).FirstOrDefault();

                Assert.IsNull(f);

                // G

                var g = (
                    from node in selfAndAncestors
                    where node.Data == "G"
                    select node).FirstOrDefault();

                Assert.IsNull(g);
            }

            /// <summary>
            /// Provides methods to test that the
            /// <see cref="TreeNode{T}.SelfAndAncestors"/>
            /// property has
            /// been properly implemented by testing it
            /// via a <see cref="TreeNode{T}"/> instance
            /// representing node "C" in the tree
            /// returned by <see cref="GetTestedTree"/>.
            /// </summary>
            public static void NodeC(TreeNode<string> root)
            {
                var target = (
                    from node in root.SelfAndDescendants
                    where node.Data == "C"
                    select node).FirstOrDefault();

                Assert.IsNotNull(target);

                var selfAndAncestors = target.SelfAndAncestors.ToArray();

                Assert.IsTrue(selfAndAncestors.Length == 2);

                // A

                var a = (
                    from node in selfAndAncestors
                    where node.Data == "A"
                    select node).FirstOrDefault();

                Assert.IsNotNull(a);

                var actual = a;

                Assert.IsTrue(actual.Children.Count == 2);
                Assert.AreEqual(actual.Data, "A");
                Assert.IsTrue(actual.IsTerminal == false);
                Assert.IsTrue(actual.IsBranch == true);
                Assert.IsTrue(actual.Level == 0);
                Assert.IsTrue(actual.Degree == 2);
                Assert.IsTrue(actual.IsRoot == true);

                // B

                var b = (
                    from node in selfAndAncestors
                    where node.Data == "B"
                    select node).FirstOrDefault();

                Assert.IsNull(b);

                // C

                var c = (
                    from node in selfAndAncestors
                    where node.Data == "C"
                    select node).FirstOrDefault();

                Assert.IsNotNull(c);

                actual = c;

                Assert.IsTrue(actual.Children.Count == 3);
                Assert.AreEqual(actual.Data, "C");
                Assert.IsTrue(actual.IsTerminal == false);
                Assert.IsTrue(actual.IsBranch == true);
                Assert.IsTrue(actual.Level == 1);
                Assert.IsTrue(actual.Degree == 3);
                Assert.IsTrue(actual.IsRoot == false);

                // D

                var d = (
                    from node in selfAndAncestors
                    where node.Data == "D"
                    select node).FirstOrDefault();

                Assert.IsNull(d);

                // E

                var e = (
                    from node in selfAndAncestors
                    where node.Data == "E"
                    select node).FirstOrDefault();

                Assert.IsNull(e);

                // F

                var f = (
                    from node in selfAndAncestors
                    where node.Data == "F"
                    select node).FirstOrDefault();

                Assert.IsNull(f);

                // G

                var g = (
                    from node in selfAndAncestors
                    where node.Data == "G"
                    select node).FirstOrDefault();

                Assert.IsNull(g);
            }

            /// <summary>
            /// Provides methods to test that the
            /// <see cref="TreeNode{T}.SelfAndAncestors"/>
            /// property has
            /// been properly implemented by testing it
            /// via a <see cref="TreeNode{T}"/> instance
            /// representing node "D" in the tree
            /// returned by <see cref="GetTestedTree"/>.
            /// </summary>
            public static void NodeD(TreeNode<string> root)
            {
                var target = (
                    from node in root.SelfAndDescendants
                    where node.Data == "D"
                    select node).FirstOrDefault();

                Assert.IsNotNull(target);

                var selfAndAncestors = target.SelfAndAncestors.ToArray();

                Assert.IsTrue(selfAndAncestors.Length == 3);

                // A

                var a = (
                    from node in selfAndAncestors
                    where node.Data == "A"
                    select node).FirstOrDefault();

                Assert.IsNotNull(a);

                var actual = a;

                Assert.IsTrue(actual.Children.Count == 2);
                Assert.AreEqual(actual.Data, "A");
                Assert.IsTrue(actual.IsTerminal == false);
                Assert.IsTrue(actual.IsBranch == true);
                Assert.IsTrue(actual.Level == 0);
                Assert.IsTrue(actual.Degree == 2);
                Assert.IsTrue(actual.IsRoot == true);

                // B

                var b = (
                    from node in selfAndAncestors
                    where node.Data == "B"
                    select node).FirstOrDefault();

                Assert.IsNull(b);

                // C

                var c = (
                    from node in selfAndAncestors
                    where node.Data == "C"
                    select node).FirstOrDefault();

                Assert.IsNotNull(c);

                actual = c;

                Assert.IsTrue(actual.Children.Count == 3);
                Assert.AreEqual(actual.Data, "C");
                Assert.IsTrue(actual.IsTerminal == false);
                Assert.IsTrue(actual.IsBranch == true);
                Assert.IsTrue(actual.Level == 1);
                Assert.IsTrue(actual.Degree == 3);
                Assert.IsTrue(actual.IsRoot == false);

                // D

                var d = (
                    from node in selfAndAncestors
                    where node.Data == "D"
                    select node).FirstOrDefault();

                Assert.IsNotNull(d);

                actual = d;

                Assert.IsTrue(actual.Children.Count == 0);
                Assert.AreEqual(actual.Data, "D");
                Assert.IsTrue(actual.IsTerminal == true);
                Assert.IsTrue(actual.IsBranch == false);
                Assert.IsTrue(actual.Level == 2);
                Assert.IsTrue(actual.Degree == 0);
                Assert.IsTrue(actual.IsRoot == false);

                // E

                var e = (
                    from node in selfAndAncestors
                    where node.Data == "E"
                    select node).FirstOrDefault();

                Assert.IsNull(e);

                // F

                var f = (
                    from node in selfAndAncestors
                    where node.Data == "F"
                    select node).FirstOrDefault();

                Assert.IsNull(f);

                // G

                var g = (
                    from node in selfAndAncestors
                    where node.Data == "G"
                    select node).FirstOrDefault();

                Assert.IsNull(g);
            }

            /// <summary>
            /// Provides methods to test that the
            /// <see cref="TreeNode{T}.SelfAndAncestors"/>
            /// property has
            /// been properly implemented by testing it
            /// via a <see cref="TreeNode{T}"/> instance
            /// representing node "G" in the tree
            /// returned by <see cref="GetTestedTree"/>.
            /// </summary>
            public static void NodeG(TreeNode<string> root)
            {
                var target = (
                    from node in root.SelfAndDescendants
                    where node.Data == "G"
                    select node).FirstOrDefault();

                Assert.IsNotNull(target);

                var selfAndAncestors = target.SelfAndAncestors.ToArray();

                Assert.IsTrue(selfAndAncestors.Length == 4);

                // A

                var a = (
                    from node in selfAndAncestors
                    where node.Data == "A"
                    select node).FirstOrDefault();

                Assert.IsNotNull(a);

                var actual = a;

                Assert.IsTrue(actual.Children.Count == 2);
                Assert.AreEqual(actual.Data, "A");
                Assert.IsTrue(actual.IsTerminal == false);
                Assert.IsTrue(actual.IsBranch == true);
                Assert.IsTrue(actual.Level == 0);
                Assert.IsTrue(actual.Degree == 2);
                Assert.IsTrue(actual.IsRoot == true);

                // B

                var b = (
                    from node in selfAndAncestors
                    where node.Data == "B"
                    select node).FirstOrDefault();

                Assert.IsNull(b);

                // C

                var c = (
                    from node in selfAndAncestors
                    where node.Data == "C"
                    select node).FirstOrDefault();

                Assert.IsNotNull(c);

                actual = c;

                Assert.IsTrue(actual.Children.Count == 3);
                Assert.AreEqual(actual.Data, "C");
                Assert.IsTrue(actual.IsTerminal == false);
                Assert.IsTrue(actual.IsBranch == true);
                Assert.IsTrue(actual.Level == 1);
                Assert.IsTrue(actual.Degree == 3);
                Assert.IsTrue(actual.IsRoot == false);

                // D

                var d = (
                    from node in selfAndAncestors
                    where node.Data == "D"
                    select node).FirstOrDefault();

                Assert.IsNull(d);

                // E

                var e = (
                    from node in selfAndAncestors
                    where node.Data == "E"
                    select node).FirstOrDefault();

                Assert.IsNull(e);

                // F

                var f = (
                    from node in selfAndAncestors
                    where node.Data == "F"
                    select node).FirstOrDefault();

                Assert.IsNotNull(f);

                actual = f;

                Assert.IsTrue(actual.Children.Count == 1);
                Assert.AreEqual(actual.Data, "F");
                Assert.IsTrue(actual.IsTerminal == false);
                Assert.IsTrue(actual.IsBranch == true);
                Assert.IsTrue(actual.Level == 2);
                Assert.IsTrue(actual.Degree == 1);
                Assert.IsTrue(actual.IsRoot == false);

                // G

                var g = (
                    from node in selfAndAncestors
                    where node.Data == "G"
                    select node).FirstOrDefault();

                Assert.IsNotNull(g);

                actual = g;

                Assert.IsTrue(actual.Children.Count == 0);
                Assert.AreEqual(actual.Data, "G");
                Assert.IsTrue(actual.IsTerminal == true);
                Assert.IsTrue(actual.IsBranch == false);
                Assert.IsTrue(actual.Level == 3);
                Assert.IsTrue(actual.Degree == 0);
                Assert.IsTrue(actual.IsRoot == false);
            }
        }

        /// <summary>
        /// Provides methods to test that the
        /// <see cref="TreeNode{T}.Ancestors"/>
        /// property has
        /// been properly implemented.
        /// </summary>
        public static class Ancestors
        {
            /// <summary>
            /// Provides methods to test that the
            /// <see cref="TreeNode{T}.Ancestors"/>
            /// property has
            /// been properly implemented by testing it
            /// via a <see cref="TreeNode{T}"/> instance
            /// representing node "A" in the tree
            /// returned by <see cref="GetTestedTree"/>.
            /// </summary>
            public static void NodeA(TreeNode<string> root)
            {
                var target = (
                    from node in root.SelfAndDescendants
                    where node.Data == "A"
                    select node).FirstOrDefault();

                Assert.IsNotNull(target);

                var ancestors = target.Ancestors.ToArray();

                Assert.IsTrue(ancestors.Length == 0);

                // A

                var a = (
                    from node in ancestors
                    where node.Data == "A"
                    select node).FirstOrDefault();

                Assert.IsNull(a);

                // B

                var b = (
                    from node in ancestors
                    where node.Data == "B"
                    select node).FirstOrDefault();

                Assert.IsNull(b);

                // C

                var c = (
                    from node in ancestors
                    where node.Data == "C"
                    select node).FirstOrDefault();

                Assert.IsNull(c);

                // D

                var d = (
                    from node in ancestors
                    where node.Data == "D"
                    select node).FirstOrDefault();

                Assert.IsNull(d);

                // E

                var e = (
                    from node in ancestors
                    where node.Data == "E"
                    select node).FirstOrDefault();

                Assert.IsNull(e);

                // F

                var f = (
                    from node in ancestors
                    where node.Data == "F"
                    select node).FirstOrDefault();

                Assert.IsNull(f);

                // G

                var g = (
                    from node in ancestors
                    where node.Data == "G"
                    select node).FirstOrDefault();

                Assert.IsNull(g);
            }

            /// <summary>
            /// Provides methods to test that the
            /// <see cref="TreeNode{T}.Ancestors"/>
            /// property has
            /// been properly implemented by testing it
            /// via a <see cref="TreeNode{T}"/> instance
            /// representing node "C" in the tree
            /// returned by <see cref="GetTestedTree"/>.
            /// </summary>
            public static void NodeC(TreeNode<string> root)
            {
                var target = (
                    from node in root.SelfAndDescendants
                    where node.Data == "C"
                    select node).FirstOrDefault();

                Assert.IsNotNull(target);

                var ancestors = target.Ancestors.ToArray();

                Assert.IsTrue(ancestors.Length == 1);

                // A

                var a = (
                    from node in ancestors
                    where node.Data == "A"
                    select node).FirstOrDefault();

                Assert.IsNotNull(a);

                var actual = a;

                Assert.IsTrue(actual.Children.Count == 2);
                Assert.AreEqual(actual.Data, "A");
                Assert.IsTrue(actual.IsTerminal == false);
                Assert.IsTrue(actual.IsBranch == true);
                Assert.IsTrue(actual.Level == 0);
                Assert.IsTrue(actual.Degree == 2);
                Assert.IsTrue(actual.IsRoot == true);

                // B

                var b = (
                    from node in ancestors
                    where node.Data == "B"
                    select node).FirstOrDefault();

                Assert.IsNull(b);

                // C

                var c = (
                    from node in ancestors
                    where node.Data == "C"
                    select node).FirstOrDefault();

                Assert.IsNull(c);

                // D

                var d = (
                    from node in ancestors
                    where node.Data == "D"
                    select node).FirstOrDefault();

                Assert.IsNull(d);

                // E

                var e = (
                    from node in ancestors
                    where node.Data == "E"
                    select node).FirstOrDefault();

                Assert.IsNull(e);

                // F

                var f = (
                    from node in ancestors
                    where node.Data == "F"
                    select node).FirstOrDefault();

                Assert.IsNull(f);

                // G

                var g = (
                    from node in ancestors
                    where node.Data == "G"
                    select node).FirstOrDefault();

                Assert.IsNull(g);
            }

            /// <summary>
            /// Provides methods to test that the
            /// <see cref="TreeNode{T}.Ancestors"/>
            /// property has
            /// been properly implemented by testing it
            /// via a <see cref="TreeNode{T}"/> instance
            /// representing node "D" in the tree
            /// returned by <see cref="GetTestedTree"/>.
            /// </summary>
            public static void NodeD(TreeNode<string> root)
            {
                var target = (
                    from node in root.SelfAndDescendants
                    where node.Data == "D"
                    select node).FirstOrDefault();

                Assert.IsNotNull(target);

                var ancestors = target.Ancestors.ToArray();

                Assert.IsTrue(ancestors.Length == 2);

                // A

                var a = (
                    from node in ancestors
                    where node.Data == "A"
                    select node).FirstOrDefault();

                Assert.IsNotNull(a);

                var actual = a;

                Assert.IsTrue(actual.Children.Count == 2);
                Assert.AreEqual(actual.Data, "A");
                Assert.IsTrue(actual.IsTerminal == false);
                Assert.IsTrue(actual.IsBranch == true);
                Assert.IsTrue(actual.Level == 0);
                Assert.IsTrue(actual.Degree == 2);
                Assert.IsTrue(actual.IsRoot == true);

                // B

                var b = (
                    from node in ancestors
                    where node.Data == "B"
                    select node).FirstOrDefault();

                Assert.IsNull(b);

                // C

                var c = (
                    from node in ancestors
                    where node.Data == "C"
                    select node).FirstOrDefault();

                Assert.IsNotNull(c);

                actual = c;

                Assert.IsTrue(actual.Children.Count == 3);
                Assert.AreEqual(actual.Data, "C");
                Assert.IsTrue(actual.IsTerminal == false);
                Assert.IsTrue(actual.IsBranch == true);
                Assert.IsTrue(actual.Level == 1);
                Assert.IsTrue(actual.Degree == 3);
                Assert.IsTrue(actual.IsRoot == false);

                // D

                var d = (
                    from node in ancestors
                    where node.Data == "D"
                    select node).FirstOrDefault();

                Assert.IsNull(d);

                // E

                var e = (
                    from node in ancestors
                    where node.Data == "E"
                    select node).FirstOrDefault();

                Assert.IsNull(e);

                // F

                var f = (
                    from node in ancestors
                    where node.Data == "F"
                    select node).FirstOrDefault();

                Assert.IsNull(f);

                // G

                var g = (
                    from node in ancestors
                    where node.Data == "G"
                    select node).FirstOrDefault();

                Assert.IsNull(g);
            }

            /// <summary>
            /// Provides methods to test that the
            /// <see cref="TreeNode{T}.Ancestors"/>
            /// property has
            /// been properly implemented by testing it
            /// via a <see cref="TreeNode{T}"/> instance
            /// representing node "G" in the tree
            /// returned by <see cref="GetTestedTree"/>.
            /// </summary>
            public static void NodeG(TreeNode<string> root)
            {
                var target = (
                    from node in root.SelfAndDescendants
                    where node.Data == "G"
                    select node).FirstOrDefault();

                Assert.IsNotNull(target);

                var ancestors = target.Ancestors.ToArray();

                Assert.IsTrue(ancestors.Length == 3);

                // A

                var a = (
                    from node in ancestors
                    where node.Data == "A"
                    select node).FirstOrDefault();

                Assert.IsNotNull(a);

                var actual = a;

                Assert.IsTrue(actual.Children.Count == 2);
                Assert.AreEqual(actual.Data, "A");
                Assert.IsTrue(actual.IsTerminal == false);
                Assert.IsTrue(actual.IsBranch == true);
                Assert.IsTrue(actual.Level == 0);
                Assert.IsTrue(actual.Degree == 2);
                Assert.IsTrue(actual.IsRoot == true);

                // B

                var b = (
                    from node in ancestors
                    where node.Data == "B"
                    select node).FirstOrDefault();

                Assert.IsNull(b);

                // C

                var c = (
                    from node in ancestors
                    where node.Data == "C"
                    select node).FirstOrDefault();

                Assert.IsNotNull(c);

                actual = c;

                Assert.IsTrue(actual.Children.Count == 3);
                Assert.AreEqual(actual.Data, "C");
                Assert.IsTrue(actual.IsTerminal == false);
                Assert.IsTrue(actual.IsBranch == true);
                Assert.IsTrue(actual.Level == 1);
                Assert.IsTrue(actual.Degree == 3);
                Assert.IsTrue(actual.IsRoot == false);

                // D

                var d = (
                    from node in ancestors
                    where node.Data == "D"
                    select node).FirstOrDefault();

                Assert.IsNull(d);

                // E

                var e = (
                    from node in ancestors
                    where node.Data == "E"
                    select node).FirstOrDefault();

                Assert.IsNull(e);

                // F

                var f = (
                    from node in ancestors
                    where node.Data == "F"
                    select node).FirstOrDefault();

                Assert.IsNotNull(f);

                actual = f;

                Assert.IsTrue(actual.Children.Count == 1);
                Assert.AreEqual(actual.Data, "F");
                Assert.IsTrue(actual.IsTerminal == false);
                Assert.IsTrue(actual.IsBranch == true);
                Assert.IsTrue(actual.Level == 2);
                Assert.IsTrue(actual.Degree == 1);
                Assert.IsTrue(actual.IsRoot == false);

                // G

                var g = (
                    from node in ancestors
                    where node.Data == "G"
                    select node).FirstOrDefault();

                Assert.IsNull(g);
            }
        }
    }
}
