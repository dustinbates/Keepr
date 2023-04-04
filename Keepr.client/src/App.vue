<template>
  <header>
    <div class="container-fluid">
      <div class="row">
        <div class="col-4 d-flex align-items-center">
          <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
            <p class="home ms-3 fs-3"> Home </p>
          </router-link>
          <div v-if="account.id" class="dropdown">
            <div class="dropdown-toggle m-0 p-0 fs-3" type="button" data-bs-toggle="dropdown" aria-expanded="false">
              Create
            </div>
            <ul class="dropdown-menu">
              <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#createKeep">New Keep</a></li>
              <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#createVault">New Vault</a>
              </li>
            </ul>
          </div>
        </div>
        <div class="col-4 d-flex justify-content-center align-items-center">
          <p class="logo">the <br> keepr <br> co.</p>
        </div>
        <div class="col-4 d-flex justify-content-end align-items-center p-3">
          <Login />
        </div>
      </div>
    </div>
  </header>
  <main>
    <router-view />
  </main>
  <footer>
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Login from './components/Login.vue'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      account: computed(() => AppState.account),
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, Login }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

header {
  box-shadow: 0px 3px 10px rgba(0, 0, 0, 0.424);
}

.home {
  background-color: #E9D8D6;
  border-radius: 15px;
  margin-right: 1em;
  margin-bottom: 0;
  padding: 0 1em;
}

.logo {
  border: 2px solid black;
  border-radius: 10px;
  width: 6em;
  padding-left: 1em;
  margin-top: 1em;
  font-family: serif;

}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
