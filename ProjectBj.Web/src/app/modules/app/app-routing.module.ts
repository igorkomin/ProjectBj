import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";

const routes: Routes = [
    {
        path: 'login',
        loadChildren: 'src/app/modules/login/login.module#LoginModule'
    },
    {
        path: 'game',
        loadChildren: 'src/app/modules/game/game.module#GameModule'
    },
    {
        path: 'logs',
        loadChildren: 'src/app/modules/logs/logs.module#LogsModule'
    },
    {
        path: 'history',
        loadChildren: 'src/app/modules/gamelogs/gamelogs.module#GamelogsModule'
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
