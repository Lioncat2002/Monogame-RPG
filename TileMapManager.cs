using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TiledSharp;

namespace RPG_1
{
    public class TileMapManager
    {
        private SpriteBatch spriteBatch;
        TmxMap map;
        Texture2D tileset;
        int tilesetTilesWide;
        int tileWidth;
        int tileHeight;

        public TileMapManager(SpriteBatch _spriteBatch,TmxMap _map, Texture2D _tileset,int _tilesetTilesWide, int _tileWidth,int _tileHeight)

        {
            spriteBatch = _spriteBatch;
            map = _map;
            tileset = _tileset;
            tilesetTilesWide = _tilesetTilesWide;
            tileWidth = _tileWidth;
            tileHeight = _tileHeight;
        }

        public void Draw(Matrix matrix)
        {
            spriteBatch.Begin(//All of these need to be here :(
                SpriteSortMode.Deferred,
                samplerState: SamplerState.PointClamp,
                effect: null,
                blendState: null,
                rasterizerState: null,
                depthStencilState: null,
                transformMatrix: matrix/*<-This is the main thing*/);
            for (var i=0;i<map.TileLayers.Count;i++)
            {
                for(var j=0;j<map.TileLayers[i].Tiles.Count;j++)
                {
                    int gid = map.TileLayers[i].Tiles[j].Gid;
                    if(gid==0)
                    {
                        //do nothing
                    }
                    else
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);
                        float x = (j % map.Width) * map.TileWidth;
                        float y = (float)Math.Floor(j / (double)map.Width) * map.TileHeight;
                        Rectangle tilesetRec = new Rectangle((tileWidth) * column, (tileHeight) * row, tileWidth, tileHeight);
                        spriteBatch.Draw(tileset,new Rectangle((int)x,(int)y,tileWidth,tileHeight),tilesetRec,Color.White);
                    }
                }
            }
            spriteBatch.End();
        }
    }
}
