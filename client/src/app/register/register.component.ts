import {Component, inject, Input, input, InputSignal, output} from '@angular/core';
import {FormsModule} from "@angular/forms";
import { HomeComponent } from '../home/home.component';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  // usersFromHomeComponent =input.required<any>();
  cancelRegister = output<boolean>();
  private accountService = inject(AccountService);
  model:any={};


  register(){
    this.accountService.register(this.model).subscribe({
      next: response => {
        console.log(response);
        this.cancel();
      },
      error: err => console.log(err)

    })
    console.log(this.model);
  }

  cancel(){
    this.cancelRegister.emit(false);
    console.log('cancelled');
  }

}







