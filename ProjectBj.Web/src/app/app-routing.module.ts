import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

const routes: Routes = [
    {
        path: 'login',
        loadChildren: 'src/app/login/login.module#LoginModule'
    },
    {
        path: 'game',
        loadChildren: 'src/app/game/game.module#GameModule'
    },
    {
        path: 'logs',
        loadChildren: 'src/app/logs/logs.module#LogsModule'
    },
    {
        path: 'history',
        loadChildren: 'src/app/history/history.module#HistoryModule'
    },
    {
        path: 'error',
        loadChildren: 'src/app/error/error.module#ErrorModule'
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
