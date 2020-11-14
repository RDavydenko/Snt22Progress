<template>
    <div class="col-md-3 col-lg-3 col-xl-3" style="padding: 0">
            <div
              class="col-md-12 menu-top d-md-block d-none"
              style="padding: 0; display: block; border-radius: 3px 3px 3px 3px"
            >
              <ul class="nav menu-top" style="border-radius: 0 0 0 5px">
                <li class="nav-item" style="width: 100%; text-align: center">
                  <router-link
                    class="nav-link menu-top-item"
                    active-class="active-item"
                    to="/docs"
                  >
                    Документы</router-link
                  >
                </li>
                <li class="nav-item" style="width: 100%; text-align: center">
                  <router-link
                    class="nav-link menu-top-item"
                    active-class="active-item"
                    to="/government"
                  >
                    Правление</router-link
                  >
                </li>
                <li class="nav-item" style="width: 100%; text-align: center">
                  <router-link
                    class="nav-link menu-top-item"
                    active-class="active-item"
                    to="/plan-scheme"
                  >
                    План-схема СНТ</router-link
                  >
                </li>
                <li class="nav-item" style="width: 100%; text-align: center">
                  <router-link
                    class="nav-link menu-top-item"
                    active-class="active-item"
                    to="/legislation"
                  >
                    Законодательство</router-link
                  >
                </li>
                <li class="nav-item" style="width: 100%; text-align: center">
                  <router-link
                    class="nav-link menu-top-item"
                    active-class="active-item"
                    to="/debtors"
                  >
                    Должники</router-link
                  >
                </li>
              </ul>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0">
              <div
                class="col-sm-12 left-nav-menu-last-news"
                style="
                  position: static;
                  padding-top: 10px;
                  padding-bottom: 10px;
                "
              >
                <h4 style="margin: 0">Последние новости</h4>
              </div>
            </div>
            <div
              class="left-nav-menu-last-news"
              style="background-color: rgb(191, 190, 78)"
            >
              <router-link
                style="
                  width: 100%;
                  height: 100%;
                  display: block;
                  color: white;
                  padding: 10px 0;
                "
                href="/list-sales"
                ><h4>Объявления о продаже участков</h4></router-link
              >
            </div>
          </div>
</template>