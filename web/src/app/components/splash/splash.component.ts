import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { inspect } from '@rxjs-insights/devtools';
import { Subject, takeUntil } from 'rxjs';
import { TokenService } from '../../services/token.service';

@Component({
  selector: 'app-splash',
  templateUrl: './splash.component.html',
  styleUrls: ['./splash.component.scss'],
})
export class SplashComponent implements OnDestroy {
  private destroyed$ = new Subject<void>();

  constructor(private tokenService: TokenService, private router: Router) {
    this.tokenService.token$
      .pipe(takeUntil(this.destroyed$), inspect)
      .subscribe(() => {
        if (this.tokenService.isLoggedIn) {
          this.router.navigateByUrl('library');
        }
      });
  }

  ngOnDestroy(): void {
    this.destroyed$.next();
  }
}
