import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { AppGlobal } from "../app.global";

@Injectable()
export class LoginService {
    global: AppGlobal;
    constructor(private http: HttpClient, private globalService: AppGlobal) {
        this.global = globalService;
    }

    async authenticateUser<T>(model: any): Promise<any> {
        let response = await this.http.post(this.global.baseAPIUrl + 'authentication', JSON.stringify(model), this.global.requestHeaders()).pipe<any>(
            catchError(error => { return this.global.handleError(error) })).toPromise();
        return response;
    }

    async otpVerify<T>(model: any): Promise<any> {
        let response = await this.http.post(this.global.baseAPIUrl + 'authentication/otp', JSON.stringify(model), this.global.requestHeaders()).pipe<any>(
            catchError(error => { return this.global.handleError(error) })).toPromise();
        return response;
    }
}