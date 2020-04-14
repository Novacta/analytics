// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.Tools;
using System.Linq;

namespace Novacta.Analytics.Infrastructure.Tests
{
    [TestClass()]
    public class TreeNodeTests
    {
        [TestMethod()]
        public void SelfAndDescendantsTest()
        {
            var root = TreeNodeTest.GetTestedTree();

            var selfAndDescendants = root.SelfAndDescendants.ToArray();

            Assert.AreEqual(selfAndDescendants.Length, 7);

            // A

            var a = (
                from node in selfAndDescendants
                where node.Data == "A" select node).FirstOrDefault();

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
                from node in selfAndDescendants
                where node.Data == "B" select node).FirstOrDefault();

            Assert.IsNotNull(b);

            actual = b;

            Assert.IsTrue(actual.Children.Count == 0);
            Assert.AreEqual(actual.Data, "B");
            Assert.IsTrue(actual.IsTerminal == true);
            Assert.IsTrue(actual.IsBranch == false);
            Assert.IsTrue(actual.Level == 1);
            Assert.IsTrue(actual.Degree == 0);
            Assert.IsTrue(actual.IsRoot == false);

            // C

            var c = (
                from node in selfAndDescendants
                where node.Data == "C" select node).FirstOrDefault();

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
                from node in selfAndDescendants
                where node.Data == "D" select node).FirstOrDefault();

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
                from node in selfAndDescendants
                where node.Data == "E" select node).FirstOrDefault();

            Assert.IsNotNull(e);

            actual = e;

            Assert.IsTrue(actual.Children.Count == 0);
            Assert.AreEqual(actual.Data, "E");
            Assert.IsTrue(actual.IsTerminal == true);
            Assert.IsTrue(actual.IsBranch == false);
            Assert.IsTrue(actual.Level == 2);
            Assert.IsTrue(actual.Degree == 0);
            Assert.IsTrue(actual.IsRoot == false);

            // F

            var f = (
                from node in selfAndDescendants
                where node.Data == "F" select node).FirstOrDefault();

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
                from node in selfAndDescendants
                where node.Data == "G" select node).FirstOrDefault();

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

        [TestMethod()]
        public void DescendantsTest()
        {
            var root = TreeNodeTest.GetTestedTree();

            var descendants = root.Descendants.ToArray();

            Assert.AreEqual(descendants.Length, 6);

            // A

            var a = (
                from node in descendants
                where node.Data == "A" select node).FirstOrDefault();

            Assert.IsNull(a);

            // B

            var b = (
                from node in descendants
                where node.Data == "B" select node).FirstOrDefault();

            Assert.IsNotNull(b);

            var actual = b;

            Assert.IsTrue(actual.Children.Count == 0);
            Assert.AreEqual(actual.Data, "B");
            Assert.IsTrue(actual.IsTerminal == true);
            Assert.IsTrue(actual.IsBranch == false);
            Assert.IsTrue(actual.Level == 1);
            Assert.IsTrue(actual.Degree == 0);
            Assert.IsTrue(actual.IsRoot == false);

            // C

            var c = (
                from node in descendants
                where node.Data == "C" select node).FirstOrDefault();

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
                from node in descendants
                where node.Data == "D" select node).FirstOrDefault();

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
                from node in descendants
                where node.Data == "E" select node).FirstOrDefault();

            Assert.IsNotNull(e);

            actual = e;

            Assert.IsTrue(actual.Children.Count == 0);
            Assert.AreEqual(actual.Data, "E");
            Assert.IsTrue(actual.IsTerminal == true);
            Assert.IsTrue(actual.IsBranch == false);
            Assert.IsTrue(actual.Level == 2);
            Assert.IsTrue(actual.Degree == 0);
            Assert.IsTrue(actual.IsRoot == false);

            // F

            var f = (
                from node in descendants
                where node.Data == "F" select node).FirstOrDefault();

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
                from node in descendants
                where node.Data == "G" select node).FirstOrDefault();

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

        [TestMethod()]
        public void SelfAndAncestorsTest()
        {
            var root = TreeNodeTest.GetTestedTree();

            TreeNodeTest.SelfAndAncestors.NodeA(root);
            TreeNodeTest.SelfAndAncestors.NodeC(root);
            TreeNodeTest.SelfAndAncestors.NodeD(root);
            TreeNodeTest.SelfAndAncestors.NodeG(root);
        }

        [TestMethod()]
        public void AncestorsTest()
        {
            var root = TreeNodeTest.GetTestedTree();

            TreeNodeTest.Ancestors.NodeA(root);
            TreeNodeTest.Ancestors.NodeC(root);
            TreeNodeTest.Ancestors.NodeD(root);
            TreeNodeTest.Ancestors.NodeG(root);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            TreeNode<string> root = new TreeNode<string>("A");
            var childB = root.AddChild("B");
            root.AddChild("C");

            var children = root.Children;
            var childC = (
                from child in children
                where child.Data == "C" select child).FirstOrDefault();

            Assert.IsNotNull(childC);

            var childD = childC.AddChild("D");

            string expected, actual;

            actual = root.ToString();
            expected = "Level: 0. Degree: 2. Data: A";
            Assert.AreEqual(expected, actual);

            actual = childB.ToString();
            expected = "Level: 1. Degree: 0. Data: B";
            Assert.AreEqual(expected, actual);

            actual = childC.ToString();
            expected = "Level: 1. Degree: 1. Data: C";
            Assert.AreEqual(expected, actual);

            actual = childD.ToString();
            expected = "Level: 2. Degree: 0. Data: D";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddChildTest()
        {
            TreeNode<string> root = new TreeNode<string>("A");
            root.AddChild("B");
            root.AddChild("C");

            var children = root.Children;

            Assert.IsTrue(children.Count == 2);

            TreeNode<string> actual;

            actual = root;

            Assert.IsTrue(actual.Children.Count == 2);
            Assert.AreEqual(actual.Data, "A");
            Assert.IsTrue(actual.IsTerminal == false);
            Assert.IsTrue(actual.IsBranch == true);
            Assert.IsTrue(actual.Level == 0);
            Assert.IsTrue(actual.Degree == 2);
            Assert.IsTrue(actual.IsRoot == true);

            actual = children[0];

            Assert.IsTrue(actual.Children.Count == 0);
            Assert.AreEqual(actual.Data, "B");
            Assert.IsTrue(actual.IsTerminal == true);
            Assert.IsTrue(actual.IsBranch == false);
            Assert.IsTrue(actual.Level == 1);
            Assert.IsTrue(actual.Degree == 0);
            Assert.IsTrue(actual.IsRoot == false);

            actual = children[1];

            Assert.IsTrue(actual.Children.Count == 0);
            Assert.AreEqual(actual.Data, "C");
            Assert.IsTrue(actual.IsTerminal == true);
            Assert.IsTrue(actual.IsBranch == false);
            Assert.IsTrue(actual.Level == 1);
            Assert.IsTrue(actual.Degree == 0);
            Assert.IsTrue(actual.IsRoot == false);
        }

        [TestMethod()]
        public void RemoveChildTest()
        {
            TreeNode<string> root = new TreeNode<string>("A");
            root.AddChild("B");
            root.AddChild("C");

            var children = root.Children;
            root.RemoveChild(children[0]);

            Assert.IsTrue(children.Count == 1);

            TreeNode<string> actual;

            actual = root;

            Assert.IsTrue(actual.Children.Count == 1);
            Assert.AreEqual(actual.Data, "A");
            Assert.IsTrue(actual.IsTerminal == false);
            Assert.IsTrue(actual.IsBranch == true);
            Assert.IsTrue(actual.Level == 0);
            Assert.IsTrue(actual.Degree == 1);
            Assert.IsTrue(actual.IsRoot == true);

            actual = children[0];

            Assert.IsTrue(actual.Children.Count == 0);
            Assert.AreEqual(actual.Data, "C");
            Assert.IsTrue(actual.IsTerminal == true);
            Assert.IsTrue(actual.IsBranch == false);
            Assert.IsTrue(actual.Level == 1);
            Assert.IsTrue(actual.Degree == 0);
            Assert.IsTrue(actual.IsRoot == false);
        }
    }
}