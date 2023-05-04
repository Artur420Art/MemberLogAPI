using System;
namespace Member.Common.Model
{
	public class ResultModel<TResult>
	{
		public TResult? Result { get; set; }
		public string? Message { get; set; }
	}

	public static class Result
	{
		public static ResultModel<TResult> From<TResult>(TResult result)
		{
			return new ResultModel<TResult> { Result = result };
		}
	}
}

