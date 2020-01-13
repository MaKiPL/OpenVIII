﻿using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpenVIII.Fields
{
    public partial class Background
    {
        #region Classes

        private class Tiles : IList<Tile>
        {
            #region Fields

            private List<Tile> tiles;

            #endregion Fields

            #region Constructors

            public Tiles() => tiles = new List<Tile>();

            public Tiles(List<Tile> tiles) => this.tiles = tiles;

            #endregion Constructors

            #region Properties

            public Point BottomRight => new Point(tiles.Max(tile => tile.X) + Tile.size, tiles.Max(tile => tile.Y) + Tile.size);
            public int Count => ((IList<Tile>)tiles).Count;

            public int Height => Math.Abs(TopLeft.Y) + BottomRight.Y;
            public bool IsReadOnly => ((IList<Tile>)tiles).IsReadOnly;

            public Point TopLeft => new Point(tiles.Min(tile => tile.X), tiles.Min(tile => tile.Y));
            public int Width => Math.Abs(TopLeft.X) + BottomRight.X;

            #endregion Properties

            #region Indexers

            public Tile this[int index] { get => ((IList<Tile>)tiles)[index]; set => ((IList<Tile>)tiles)[index] = value; }

            #endregion Indexers

            #region Methods

            public static Tiles Load(byte[] mapb, byte textureType)
            {
                Tiles tiles = new Tiles();
                //128x256
                //using (BinaryReader pbsmim = new BinaryReader(new MemoryStream(mimb))
                using (BinaryReader pbsmap = new BinaryReader(new MemoryStream(mapb)))
                    while (pbsmap.BaseStream.Position + 16 < pbsmap.BaseStream.Length)
                    {
                        Tile tile = Tile.Load(pbsmap, tiles.Count, textureType);
                        if (tile != null)
                            tiles.Add(tile);
                    }
                return tiles;
            }

            public void Add(Tile item) => ((IList<Tile>)tiles).Add(item);

            public void Clear() => ((IList<Tile>)tiles).Clear();

            public bool Contains(Tile item) => ((IList<Tile>)tiles).Contains(item);

            public void CopyTo(Tile[] array, int arrayIndex) => ((IList<Tile>)tiles).CopyTo(array, arrayIndex);

            public IEnumerator<Tile> GetEnumerator() => ((IList<Tile>)tiles).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => ((IList<Tile>)tiles).GetEnumerator();

            public int IndexOf(Tile item) => ((IList<Tile>)tiles).IndexOf(item);

            public void Insert(int index, Tile item) => ((IList<Tile>)tiles).Insert(index, item);

            public bool Remove(Tile item) => ((IList<Tile>)tiles).Remove(item);

            public void RemoveAt(int index) => ((IList<Tile>)tiles).RemoveAt(index);

            #endregion Methods
        }

        #endregion Classes
    }
}