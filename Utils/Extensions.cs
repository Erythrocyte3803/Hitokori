﻿using osu.Framework.Graphics;
using osu.Game.Rulesets.Scoring;
using osuTK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace osu.Game.Rulesets.Hitokori.Utils {
	public static class Extensions {
		/// <summary>
		/// Sets the anchor and origin to centre.
		/// </summary>
		public static T Center<T> ( this T self ) where T : Drawable {
			self.Anchor = Anchor.Centre;
			self.Origin = Anchor.Centre;

			return self;
		}

		public static double ToRadians ( this double self )
			=> self * Math.PI / 180;
		public static double ToDegrees ( this double self )
			=> self / Math.PI * 180;

		public static float ToRadiansF ( this double self )
			=> (float)self.ToRadians();
		public static float ToDegreesF ( this double self )
			=> (float)self.ToDegrees();

		public static double ToRadians ( this float self )
			=> self * Math.PI / 180;
		public static double ToDegrees ( this float self )
			=> self / Math.PI * 180;

		public static float ToRadiansF ( this float self )
			=> (float)self.ToRadians();
		public static float ToDegreesF ( this float self )
			=> (float)self.ToDegrees();

		public static HitResult OrBetter ( this HitResult self, HitResult other )
			=> (HitResult)Math.Max( (int)self, (int)other );

		public static bool IsAbout ( this double A, double B, double range )
			=> ( A >= B - range ) && ( A <= B + range );

		public static readonly Random random = new Random();
		public static T Random<T> ( this IEnumerable<T> self )
			=> self.Skip( random.Next( 0, self.Count() ) ).First();

		public static void AddNewKeys<TA, TB> ( this Dictionary<TA, TB> self, IEnumerable<TA> keys, Func<TA,TB> items ) {
			foreach ( var key in keys ) {
				if ( !self.ContainsKey( key ) ) self.Add( key, items( key ) );
			}
		}

		public static void Swap<T> ( ref T from, ref T to ) {
			T temp = to;
			to = from;
			from = temp;
		}

		public static Vector2 Sum<T> ( this IEnumerable<T> self, Func<T, Vector2> selector )
			=> self.Aggregate( Vector2.Zero, ( a, b ) => a + selector( b ) );
		public static Vector2 Sum ( this IEnumerable<Vector2> self )
			=> self.Aggregate( Vector2.Zero, ( a, b ) => a + b );

		public static Vector2 Average<T> ( this IEnumerable<T> self, Func<T, Vector2> selector )
			=> self.Sum( selector ) / self.Count();
		public static Vector2 Average ( this IEnumerable<Vector2> self )
			=> self.Sum() / self.Count();
	}
}
