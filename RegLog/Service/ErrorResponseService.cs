﻿using RegLog.ViewModel;

namespace RegLog.Service
{
    public class ErrorResponseService
    {
        public CustomeErrorResponseViewModel CreateErrorResponse (int status, string error)
        {
            return new CustomeErrorResponseViewModel
            {
                Status = status,
                Error = error,
                Data = null
            };
        }
    }
}
