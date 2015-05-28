using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Assets
{
	// A bigger unit was needed to work with the scale of unity. This set of classes
	// attempts to resolve this issue by creating a larger standard unit.

	// This is the mass of the sun. (S)un(M)ass.
	class SM
	{
		private int _mass = 0;

		public SM(int massInSM)
		{
			_mass = massInSM;
		}

		public long GetMassAsKG()
		{
			return _mass * 1989000000000000000000000000000
		}
	}
	// This is the radius of the sun. (R)adius(S)un.
	class RS
	{

		// The radius in RS.
		private int _radius = 0;

		public RS(int distanceInRS)
		{
			_radius = distanceInRS;
		}

		public int GetDistanceAsRS()
		{
			return _radius;
		}

		public int GetDistanceAsKM()
		{
			return _radius * 695800;
		}

		public int GetDistanceAsM()
		{
			return _radius * 695800000;
		}

		public void SetDistanceFromRS(int RS)
		{
			_radius = RS;
		}

		public void SetDistanceFromKM(int kilometers)
		{
			_radius = kilometers / 695800;
		}

		public void SetDistanceFromM(int meters)
		{
			_radius = meters / 695800000;
		}
	}
}
