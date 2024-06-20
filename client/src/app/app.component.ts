import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,NgFor],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  http = inject(HttpClient);
  title = 'Dating App';
  users:any

  ngOnInit(): void {
    this.http.get('https://localhost:7070/api/Users').subscribe({
      next: response => this.users = response,
      error: err => console.log(err),
      complete: () =>console.log('request complete')
    })
  }



}
