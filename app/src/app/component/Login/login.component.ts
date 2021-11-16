import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { NotificationService } from 'src/app/services/notification.service';
import { LoginService } from 'src/app/services/login.service';
import { GeneralSettingService } from 'src/app/services/generalsetting.service';
import { AppGlobal } from "../../app.global";

@Component({
    selector: 'app-root',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    exportAs: 'loginForm'
})

export class LoginComponent implements OnInit {
    global: AppGlobal;
    errorMessage: any;
    isLogin: boolean = true;
    isOtp: boolean = false;
    public model: AuthenticationModel = new AuthenticationModel;
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private globalService: AppGlobal,
        private notificationService: NotificationService,
        private spinner: NgxSpinnerService,
        private loginService: LoginService,
        private generalSettingService: GeneralSettingService
    ) { this.global = globalService; }

    async ngOnInit() { await this.clearData(); };

    async clearData() {
        this.isLogin = true;
        this.isOtp = false;
        this.model = {
            user_id: '',
            user_name: '',
            user_password: '',
            user_mobileno: '',
            otp: ''
        };

        await this.getDefaultMobileNo();
    };

    async getDefaultMobileNo() {
        this.spinner.show();
        const data = await this.generalSettingService.getDefaultMobileNo().catch(error => { if (error.code === 404) this.notificationService.showError(error.message); });
        if (data !== null && data !== undefined) {
            this.model.user_mobileno = data["data"];
            this.spinner.hide();
        }
        else
            this.spinner.hide();
    };

    checkValidation() {
        var message;
        if (this.model.user_name === "")
            message = 'Please enter user name.';
        else if (this.model.user_password === "")
            message = 'Please enter valid password.';
        else if (this.model.user_mobileno === undefined || this.model.user_mobileno === null || this.model.user_mobileno === '')
            message = 'Please enter mobile number.';
        else if (!this.validateMobileNoWithPlus(this.model.user_mobileno))
            message = 'Please enter valid mobile number.';
        else
            message = '';

        return message;
    };

    validateMobileNoWithPlus(mobileNo: any) {
        var mobileReg = /^([0|\+[0-9]{1,5})?([0-9]{10})$/;
        return mobileReg.test(mobileNo);
    }

    async onSubmit() {
        this.spinner.show();
        const message = this.checkValidation();
        if (message !== '') {
            this.notificationService.showWarning(message);
            this.spinner.hide();
            return false;
        }
        else {
            const param = {
                "user_name": this.model.user_name,
                "user_password": this.model.user_password,
                "user_mobileno": this.model.user_mobileno
            };
            const response = await this.loginService.authenticateUser(param).catch(error => { });
            if (response !== null && response !== undefined && response !== '') {
                if (response["code"] === 1) {
                    await this.clearData();
                    this.notificationService.showSuccess(response["message"]);
                    this.spinner.hide();
                    this.isLogin = false;
                    this.isOtp = true;
                    this.model.user_id = response["data"]["user_id"];
                    sessionStorage.setItem('userId', this.model.user_id)
                    return false;
                }
                else {
                    this.notificationService.showError(response["message"]);
                    this.spinner.hide();
                    return false;
                }
            }
            this.spinner.hide();
            return false;
        }
    };

    onLoginKeyUp(e: any) {
        if (e.keyCode === 13)
            this.onSubmit();
    }

    checkOtpValidation() {
        var message;
        if (this.model.user_id === "")
            message = 'Please enter user.';
        else if (this.model.otp === "")
            message = 'Please enter OTP.';
        else
            message = '';

        return message;
    };

    async otpVerify() {
        this.spinner.show();
        const message = this.checkOtpValidation();
        if (message !== '') {
            this.notificationService.showWarning(message);
            this.spinner.hide();
            return false;
        }
        else {
            const param = {
                "user_id": this.model.user_id,
                "otp": this.model.otp,
            };
            const response = await this.loginService.otpVerify(param).catch(error => { });
            if (response !== null && response !== undefined && response !== '') {
                if (response["code"] === 1) {
                    this.notificationService.showSuccess(response["message"]);
                    this.spinner.hide();
                    //this.router.navigateByUrl('/landing');
                    window.location.href = '/landing';
                    return false;
                }
                else {
                    this.notificationService.showError(response["message"]);
                    this.spinner.hide();
                    this.isLogin = true;
                    this.isOtp = false;
                    return false;
                }
            }
            this.spinner.hide();
            return false;
        }
    };

    onOtpKeyUp(e: any) {
        if (e.keyCode === 13)
            this.otpVerify();
    }
}
export class AuthenticationModel {
    user_id: string = '';
    user_name: string = '';
    user_password: string = '';
    user_mobileno: string = '';
    otp: string = '';
}