using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace HeroBarn
{
    class ObservableCollectionConverter
    {
        /// <summary>
        /// Takes an enumerable and creates an observable collection.
        /// </summary>
        /// <typeparam name="T">Type of elements in the enumerable</typeparam>
        /// <param name="coll">collection to be concerted</param>
        /// <returns></returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> coll)
        {
            var c = new ObservableCollection<T>();
            foreach (var e in coll) c.Add(e);
            return c;
        }
    }
}
