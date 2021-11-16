import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
    providedIn: 'root'
})
export class NotificationService {
    title: string = ''
    constructor(private toastr: ToastrService) { }

    showSuccess(message?: string) {
        this.toastr.success(message, this.title, { timeOut: 3000 });
    }

    showError(message?: string) {
        this.toastr.error(message, this.title, { timeOut: 3000 });
    }

    showInfo(message?: string) {
        this.toastr.info(message, this.title, { timeOut: 3000 });
    }

    showWarning(message?: string) {
        this.toastr.warning(message, this.title, { timeOut: 3000 });
    }
}