using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Player.Animations
{
    /// <summary>
    /// A string used to set the property path of an animation to a text effect. This class is a
    /// helper class for the WordAnimations class.
    /// </summary>
    public class AnimationTargetString
    {
        /// <summary>
        /// The property of the text effect to target.
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// The index of the text effect in a WordAnimation.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Constructs an instance of AnimationTargetString with a given string Property and Index
        /// </summary>
        /// <param name="property">The property of the text effect to target.</param>
        /// <param name="index">The index of the text effect in a WordAnimation.</param>
        public AnimationTargetString(string property, int index)
        {
            this.Property = property;
            this.Index = index;
        }

        /// <summary>
        /// Returns the string used to construct a PropertyPath based on this AnimationTargetString.
        /// </summary>
        /// <param name="offsetIndex">The last size of a System.Windows.Controls.Textblock's text 
        /// effect collection, not including all of the elements in the current WordAnimation.
        /// </param>
        /// <returns>The PropertyPath string represented by this class.</returns>
        public string PropertyString(int offsetIndex)
        {
            return String.Format("TextEffects[{0}].{1}", offsetIndex + Index, Property);
        }

        /// <summary>
        /// Returns an instance of the PropertyPath class constructed using this object's 
        /// PropertyString method.
        /// </summary>
        /// <param name="offsetIndex">The last size of a System.Windows.Controls.Textblock's text 
        /// effect collection, not including all of the elements in the current WordAnimation.
        /// </param>
        /// <returns>A PropertyPath represented by this class.</returns>
        public PropertyPath PropertyPath(int offsetIndex)
        {
            return new PropertyPath(this.PropertyString(offsetIndex));
        }

        /// <summary>
        /// Returns this class represented as a string. NOTE: not the same as the PropertyString
        /// method.
        /// </summary>
        /// <returns>This class represented as a string.</returns>
        public override string ToString()
        {
            return String.Format("TextEffect property '{0}' at element {1}", Property, Index);
        }
    }
}
