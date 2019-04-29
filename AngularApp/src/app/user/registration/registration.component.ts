import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  constructor(private userService: UserService,
              private toastr: ToastrService) { }

  ngOnInit() {
    this.userService.formModel.reset();
  }

  onSubmit() {
    this.userService.register().subscribe(
      (res: any) => {
        if (res.succeeded) {
          this.userService.formModel.reset();
          this.toastr.success('New user created', 'Registration successful');
        } else {
          res.errors.forEach(el => {
            switch (el.code) {
              case 'DuplicateUserName':
                //User name is already taken
                this.toastr.error('User name is already taken', 'Registration failed');
                break;
              default:
                this.toastr.error(el.description, 'Registration failed');
                break;
            }
          })
        }
      },
      err => {
        console.log(err);
      }
    );
  }

}
