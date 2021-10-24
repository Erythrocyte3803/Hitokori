﻿using osu.Framework.Graphics;
using osu.Game.Rulesets.Hitokori.Orbitals.Events;
using System;

namespace osu.Game.Rulesets.Hitokori.Collections {
	public class AnimatedValue<T> where T : struct {
		private T value;
		private readonly VisualEventSeeker events = new() { ModifiedBehaviour = TimelineModifiedBehaviour.Replay };

		private static Func<AnimatedValue<T>, T> getFunc = t => t.value;
		private static Action<AnimatedValue<T>, T> setFunc = (t,v) => t.value = v;
		private static string[] categories = new string[] { "Value" };

		public T ValueAt ( double time ) {
			events.CurrentTime = time;
			events.Apply();

			return value;
		}

		public TimelineSeeker<VisualEvent>.Entry Animate ( double time, T value, double duration = 0, Easing easing = Easing.None ) {
			return events[ events.Add( time, duration, new GenericVisualEvent<AnimatedValue<T>, T>( time, this, value, getFunc, setFunc, categories, duration, easing ) ) ];
		}
		public void RemoveAnimation ( TimelineSeeker<VisualEvent>.Entry entry ) {
			events.Remove( entry );
		}
		public void Clear () {
			events.Clear();
		}
	}
}
