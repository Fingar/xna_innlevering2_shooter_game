using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Innlevering_2
{
    public class ContentLoader<T>
    {
        public Game Game { get; protected set; }
        
        private Dictionary<string, T> content;

        public ContentLoader(Game game)
        {
            Game = game;
            content = new Dictionary<string, T>();
        }

        public T get(String key)
        {
            if (content.ContainsKey(key))
            {
                return content[key];
            }
            else
            {
                content.Add(key, Game.Content.Load<T>(key));
                return content[key];
            } 
        }

        public void Load(string key)
        {
            content.Add(key, Game.Content.Load<T>(key));
        }

    }
}
