import { Injectable, OnDestroy } from '@angular/core';
import { inspect } from '@rxjs-insights/devtools';
import { BehaviorSubject, Subject, takeUntil } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TokenService implements OnDestroy {
  public token$: BehaviorSubject<string>;

  private destroyed$ = new Subject<void>();

  public get isLoggedIn(): boolean {
    return this.token$.getValue().length > 0;
  }

  constructor() {
    this.token$ = new BehaviorSubject(localStorage.getItem('jwt') ?? '');

    this.token$.pipe(takeUntil(this.destroyed$), inspect).subscribe((token) => {
      if (token.length > 0) {
        localStorage.setItem('jwt', token);
      } else {
        localStorage.removeItem('jwt');
      }
    });
  }

  ngOnDestroy(): void {
    this.destroyed$.next();
  }
}
