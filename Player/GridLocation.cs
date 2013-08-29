﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
