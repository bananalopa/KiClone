﻿namespace Kingdom
{
	public interface IPoolable
	{
		public void Activate();
		
		public void Deactivate();
		
		public void Destroy();
	}
}