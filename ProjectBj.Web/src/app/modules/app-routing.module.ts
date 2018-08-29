import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";

const routes: Routes = [
    {
        path: 'login',
        loadChildren: './login/login.module#LoginModule'
    },
    {
        path: 'game', 
        loadChildren: './game/game.module#GameModule'
    },
    {
        path: 'logs',
        loadChildren: './logs/logs.module#LogsModule'
    },
    { path: "", redirectTo: "/login", pathMatch: "full" },
    { path: "**", redirectTo: "/login" }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],

    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }
