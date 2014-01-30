using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibEnACT;

namespace Player
{
    public class GridLocation
    {
        public static readonly GridLocation BottomLeft   = new GridLocation(2, 1, 0, 2);
        public static readonly GridLocation BottomCentre = new GridLocation(2, 1, 0, 3);
        public static readonly GridLocation BottomRight  = new GridLocation(2, 1, 1, 2);
        public static readonly GridLocation MiddleLeft   = new GridLocation(1, 1, 0, 2);
        public static readonly GridLocation MiddleCentre = new GridLocation(1, 1, 0, 3);
        public static readonly GridLocation MiddleRight  = new GridLocation(1, 1, 1, 2);
        public static readonly GridLocation TopLeft      = new GridLocation(0, 1, 0, 2);
        public static readonly GridLocation TopCentre    = new GridLocation(0, 1, 0, 3);
        public static readonly GridLocation TopRight     = new GridLocation(0, 1, 1, 2);

        /// <summary>
        /// This Dictionary provides a 1 to 1 mapping from the ScreenLocation enum to it's
        /// equivalent grid location.
        /// </summary>
        public static readonly Dictionary<ScreenLocation, GridLocation> ScreenLocationMap
            = new Dictionary<ScreenLocation, GridLocation>
        {
            {ScreenLocation.BottomLeft, GridLocation.BottomLeft},
            {ScreenLocation.BottomCentre, GridLocation.BottomCentre},
            {ScreenLocation.BottomRight, GridLocation.BottomRight},
            {ScreenLocation.MiddleLeft, GridLocation.MiddleLeft},
            {ScreenLocation.MiddleCentre, GridLocation.MiddleCentre},
            {ScreenLocation.MiddleRight, GridLocation.MiddleRight},
            {ScreenLocation.TopLeft, GridLocation.TopLeft},
            {ScreenLocation.TopCentre, GridLocation.TopCentre},
            {ScreenLocation.TopRight, GridLocation.TopRight},
        };

        /// <summary>
        /// Translates a Screenlocation value into a GridLocation value using the ScreenLocationMap.
        /// </summary>
        /// <param name="location">The location to translate.</param>
        /// <returns>The translated value.</returns>
        public static GridLocation GetGridLocation(ScreenLocation location)
        {
            return ScreenLocationMap[location];
        }

        /// <summary>
        /// The row position.
        /// </summary>
        public int Row { get; private set; }
        /// <summary>
        /// The row span.
        /// </summary>
        public int RowSpan { get; private set; }
        /// <summary>
        /// The column position.
        /// </summary>
        public int Column { get; private set; }
        /// <summary>
        /// The column span.
        /// </summary>
        public int ColumnSpan { get; private set; }

        /// <summary>
        /// Initializes an instance of the GridLocation class with the specified parameters.
        /// </summary>
        /// <param name="row">The row position.</param>
        /// <param name="rowSpan">The row span.</param>
        /// <param name="column">The column position.</param>
        /// <param name="columnSpan">The column span.</param>
        public GridLocation(int row, int rowSpan, int column, int columnSpan)
        {
            Row = row;
            RowSpan = rowSpan;
            Column = column;
            ColumnSpan = columnSpan;
        }
    }
}
