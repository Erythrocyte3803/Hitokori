﻿using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Handlers;
using osu.Game.Rulesets.Hitokori.Objects.Base;
using osu.Game.Rulesets.Hitokori.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace osu.Game.Rulesets.Hitokori.Objects.Drawables.Trails {
	public abstract class TrailRenderer : CompositeDrawable {
		public TrailRenderer () {
			progress = new( this );
			progress.BindValueChanged( v => isInvalidated = true );
		}

		protected bool isInvalidated = true;
		protected AnimatedVector progress;
		private float trailRadius = HitokoriTile.SIZE / 8f;
		public float TrailRadius {
			get => trailRadius;
			set {
				if ( trailRadius == value ) return;

				trailRadius = value;
				isInvalidated = true;
			}
		}

		protected abstract void render ();

		protected override void Update () {
			if ( isInvalidated ) {
				isInvalidated = false;
				render();
			}

			base.Update();
		}

		public void Connect ( double duration, Easing easing = Easing.None ) {
			progress.AnimateBTo( 1, duration, easing );
		}
		public void Disconnect ( double duration, Easing easing = Easing.None ) {
			progress.AnimateATo( 1, duration, easing );
		}

		public virtual double Appear ( double duration = 500 ) {
			this.FadeInFromZero( duration );
			Connect( duration, Easing.In );

			return duration;
		}
		public virtual double Disappear ( double duration = 300 ) {
			this.FadeOut( duration );
			Disconnect( duration, Easing.Out );

			return duration;
		}
	}
}
