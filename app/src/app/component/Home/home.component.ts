import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NotificationService } from 'src/app/services/notification.service';
import { HomeService } from 'src/app/services/home.service';
import { AppGlobal } from "../../app.global";
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
    selector: 'app-root',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    exportAs: 'homeForm'
})

export class HomeComponent implements OnInit {
    global: AppGlobal;
    p: any;
    pageSize: number = 10;
    errorMessage: any;
    userId: any;
    products: any[] = [];
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private globalService: AppGlobal,
        private notificationService: NotificationService,
        private spinner: NgxSpinnerService,
        private homeService: HomeService
    ) {
        this.global = globalService;
        this.userId = (sessionStorage.getItem("userId") !== null && sessionStorage.getItem("userId") !== '' && sessionStorage.getItem("userId") !== undefined) ? sessionStorage.getItem("userId") : '';
        if (this.userId === '')
            this.router.navigateByUrl('/');
    };

    ngOnInit() { this.bindProducts(); };

    async bindProducts() {
        this.spinner.show();
        const data = await this.homeService.getProducts().catch(error => { if (error.code === 404) this.notificationService.showError(error.message); });
        if (data !== null && data !== undefined) {
            this.products = data["data"];
            this.spinner.hide();
        }
        else
            this.spinner.hide();
    };

    logOut() {
        sessionStorage.removeItem('userId');
        sessionStorage.clear();
        this.router.navigateByUrl('/');
    };
}