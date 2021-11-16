import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { AppGlobal } from "../app.global";

@Injectable()
export class HomeService {
    global: AppGlobal;
    constructor(private http: HttpClient, private globalService: AppGlobal) {
        this.global = globalService;
    }

    async getProducts<T>(): Promise<any> {
        let response = await this.http.get(this.global.baseAPIUrl + 'product', this.global.requestHeaders()).pipe<any>(
            catchError(error => { return this.global.handleGetError(error) })).toPromise();
        return response;
    }
}