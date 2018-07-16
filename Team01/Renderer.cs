using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;//Assert用

namespace Game1
{
    class Renderer
    {
        private ContentManager contentManager;//コンテンツ管理者
        private GraphicsDevice graphicsDevice;//グラフィック機器
        private SpriteBatch spriteBatch;//スプライト一括描画用オブジェクト

        //複数画像管理用変数の宣言と生成
        private Dictionary<string, Texture2D> textures = new
            Dictionary<string, Texture2D>();

        //<summary>
        //コンストラクタ
        //</summary>
        //<param name="content">Game1クラスのコンテンツ管理者</param>
        //<param name="graphics">Game1のグラフィック機器</param>
        public Renderer(ContentManager content, GraphicsDevice graphics)
        {
            contentManager = content;
            graphicsDevice = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }
        /// <summary>
        /// 画像の読み込み
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="filepath"></param>
        public void LoadContent(string assetName, string filepath = "./")
        {
            //すでにキー(assentName:アセット名)が登録されているとき
            if (textures.ContainsKey(assetName))
            {
#if DEBUG //DEBUGモードの時のみ下記エラー文をコンソールへ表示
                Console.WriteLine(assetName + "はすでに読み込まれています。\n" +
                    "                    プログラムを確認してください。");
#endif
                //それ以上読み込まないのでここで終了
                return;
            }
            //画像の読み込みとDictionaryへアセット名と画像を登録
            textures.Add(assetName, contentManager.Load<Texture2D>(filepath + assetName));
        }
        /// <summary>
        /// アンロード
        /// </summary>
        public void Unload()
        {
            textures.Clear();//Dictionaryの情報をクリア
        }
        /// <summary>
        /// 
        /// </summary>
        public void Begin()
        {
            spriteBatch.Begin();
        }
        /// <summary>
        /// 描画終了
        /// </summary>
        public void End()
        {
            spriteBatch.End();
        }

        public void DrawTexture(string assetName, Vector2 position, float
           alpha = 1.0f)
        {
            //デバッグモードの時のみ、画像描画前のアセット名チェック
            Debug.Assert(
                textures.ContainsKey(assetName),
                "描画時にアセット名の指定を間違えたか、画像の読み込み自体できていません");

            spriteBatch.Draw(textures[assetName], position, Color.White *
                alpha);
        }
        /// <summary>
        /// 画像の描画(画像を指定範囲内だけ描画)
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="position"></param>
        /// <param name="rect"></param>
        /// <param name="alpha"></param>
        public void DrawTexture(string assetName, Vector2 position,
            Rectangle rect, float alpha = 1.0f)
        {
            //デバッグモードの時のみ、画像描画前のアセット名チェック
            Debug.Assert(
                textures.ContainsKey(assetName),
                "描画時にアセット名の指定を間違えたか、画像の読み込み自体できていません");

            spriteBatch.Draw(
                textures[assetName],
                position,
                rect,
                    //指定範囲(短形で指定：左上の座標、幅、高さ)
                    Color.White * alpha);//透明値
        }
    }
}
