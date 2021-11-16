import { Injectable } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";
import { HttpHeaders } from '@angular/common/http';
import { environment } from '../environments/environment';
import { throwError } from 'rxjs';
import { NotificationService } from './services/notification.service';

@Injectable()
export class AppGlobal {
    readonly baseAPIUrl: string = '';
    userId: any = '';

    constructor(
        private spinner: NgxSpinnerService,
        private notificationService: NotificationService
    ) { this.baseAPIUrl = environment.apiEndpoint; }

    requestHeaders(token = ''): { headers: HttpHeaders | { [header: string]: string | string[]; } } {
        if (token !== '') {
            const headers = new HttpHeaders()
                .set('content-type', 'application/json')
                .set('Authorization', 'Bearer ' + token);

            return { headers };
        }
        else {
            const headers = new HttpHeaders().set('content-type', 'application/json')
            return { headers };
        }
    };

    isMobileNumberKey(evt: any) {
        if ((evt.charCode >= 48 && evt.charCode <= 57) || evt.charCode == 43) {
            return true;
        }
        return false;
    }

    isNumberKey(evt: any) {
        if (evt.charCode >= 48 && evt.charCode <= 57) {
            return true;
        }
        return false;
    }

    handleGetError(error: any) {
        this.spinner.hide();
        return throwError((error.error !== null && error.error !== undefined && error.error !== '') ? error.error : error);
    }

    handleError(error: any) {
        let message = '';
        if (error.error !== null && (error.error.message !== undefined && error.error.message !== null && error.error.message !== ''))
            message = error.error.message;
        else if (error.error !== null && (error.error.data !== null && (error.error.data.message !== undefined && error.error.data.message !== null && error.error.data.message !== '')))
            message = error.error.data.message;
        else
            message = error.message;
        this.notificationService.showError(message);
        this.spinner.hide();
        return throwError((error.error !== null && error.error !== undefined && error.error !== '') ? error.error : error);
    }
}
