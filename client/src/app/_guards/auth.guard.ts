import { CanActivateFn } from '@angular/router';
import {inject} from "@angular/core";
import {ToastrService} from "ngx-toastr";
import { AccountService } from '../_services/account.service';

export const authGuard: CanActivateFn = (route,
                                         state) => {
  const toast = inject(ToastrService);
  const accountService = inject(AccountService);

  if (accountService.currentUser()){
    return true;

  }else {
    toast.error('you shall not pass')
    return false;
  }

};








