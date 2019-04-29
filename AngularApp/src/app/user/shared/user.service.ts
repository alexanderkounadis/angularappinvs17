import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb: FormBuilder,
    private http: HttpClient)
  { }
  readonly baseURI = 'http://localhost:5000/api';

  public formModel = this.fb.group({
    UserName: ['', Validators.required],
    Email: ['', [Validators.required,Validators.email]],
    FullName: ['', Validators.required],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required]
    }, {validator: this.comparePasswords})
  });

  comparePasswords(fg: FormGroup) {
    let confirmPwdCtrl = fg.get('ConfirmPassword');
    if (confirmPwdCtrl.errors == null || 'passwordMismatch' in confirmPwdCtrl.errors) {
      if (fg.get('Password').value != confirmPwdCtrl.value) {
        confirmPwdCtrl.setErrors({ passwordMismatch: true });
      } else {
        confirmPwdCtrl.setErrors(null);
      }
    }
  }

  register() {
    var payload = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      FullName: this.formModel.value.FullName,
      Password: this.formModel.value.Passwords.Password
    };
    return this.http.post(this.baseURI + '/ApplicationUser/Register', payload);
  }
}
