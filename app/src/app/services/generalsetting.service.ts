import { catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { AppGlobal } from "../app.global";

@Injectable()
export class GeneralSettingService {
    global: AppGlobal;
    constructor(private http: HttpClient, private globalService: AppGlobal) {
        this.global = globalService;
    }

    async getDefaultMobileNo<T>(): Promise<any> {
        let response = await this.http.get(this.global.baseAPIUrl + 'generalsetting/mobileno', this.global.requestHeaders()).pipe<any>(
            catchError(error => { return this.global.handleGetError(error) })).toPromise();
        return response;
    }
}