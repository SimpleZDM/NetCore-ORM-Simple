using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDT.VirtualSoftPlatform.Common
{
    public static class ConvertTreeExtension
    {
        public static List<T> ConvertTree<T>(this List<T> trees) where T : IConvertTree<T>
        {
            if (trees == null || trees.Count == 0)
            {
                return null;
            }
            List<T> roots = trees.Where(t => t.ParentID == Guid.Empty.ToString()).ToList();
            trees = trees.Where(t => t.ParentID != Guid.Empty.ToString()).ToList();
            foreach (T root in roots)
            {
                ConvertTree(root, trees);
            }
            return roots;
        }

        private static void ConvertTree<T>(T root, List<T> trees) where T : IConvertTree<T>
        {
            if (trees==null || trees.Count==0)
            {
                return;
            }

            if (trees.Where(t=>t.ParentID==root.ID).Any())
            {
                List<T> Children = trees.Where(t=>t.ParentID==root.ID).ToList();
                trees = trees.Where(t=>t.ParentID!=root.ID).ToList();
                root.Children.AddRange(Children);
                foreach (T child in Children)
                {
                    ConvertTree(child,trees);
                }
            }
        }


    }
}
