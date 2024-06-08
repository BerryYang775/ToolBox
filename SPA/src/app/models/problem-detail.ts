export interface APIOperationError {
    process:  string;
    type:     string;
    title:    string;
    status:   number;
    detail:   string;
    instance: string;
    traceId:  string;
}

export interface DataValidationError {
    reasons:  string[];
    type:     string;
    title:    string;
    status:   number;
    detail:   string;
    instance: string;
    traceId:  string;
}