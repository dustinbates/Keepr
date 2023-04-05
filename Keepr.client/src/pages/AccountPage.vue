<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12 text-end">
        <router-link :to="{ name: 'Edit Account' }">
          <i class="mdi mdi-pencil fs-3"></i>
        </router-link>
      </div>
    </div>
    <div class="row m-md-5">
      <h1>Vaults</h1>
      <div v-for="v in myVaults" class="col-6 col-md-3 mb-4">
        <div class="vaultCard rounded">
          <router-link :to="{ name: 'Vault', params: { vaultId: v.id } }">
            <img class="vaultImg img-fluid rounded" :src="v.img" :alt="v.name">
          </router-link>
          <p class="overlay">{{ v.name }}</p>
        </div>
      </div>
    </div>
    <h1 class="px-md-5 ms-md-3">Keeps</h1>
    <section class="bricks">
      <div v-for="k in keeps">
        <KeepCard :keep="k" />
      </div>
    </section>
    <Modal id="activeKeep">
      <ActiveKeep />
    </Modal>
    <Modal id="createKeep">
      <CreateKeepForm />
    </Modal>
    <Modal id="createVault">
      <CreateVaultForm />
    </Modal>
  </div>
</template>

<script>
import { computed, onMounted, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { accountService } from '../services/AccountService'
export default {
  setup() {
    async function getKeeps() {
      try {
        const id = AppState.account.id
        await keepsService.getProfileKeeps(id)
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }
    async function getMyVaults() {
      try {
        await accountService.getMyVaults()
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }
    onMounted(() => {
      getKeeps()
      getMyVaults()
    })
    watchEffect(() => {
      getKeeps()
    })

    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.profileKeeps),
      myVaults: computed(() => AppState.myVaults),
    }
  }
}
</script>

<style lang="scss" scoped>
img {
  max-width: 100px;
}

.bricks {
  columns: 4 200px;
  column-gap: 1.5rem;
  margin-right: 5rem;
  margin-left: 5rem;

  @media screen and (max-width: 456px) {
    columns: 2;
    margin-right: .25rem;
    margin-left: .25rem;
  }

  &>div {
    width: 150px;
    color: white;
    margin: 0 1.5rem 1.5rem 0;
    display: inline-block;
    width: 100%;
  }

}

.vaultCard {
  position: relative;
}

.vaultImg {
  height: 20vh;
  min-width: 20vw;
  object-fit: cover;
  box-shadow: 0px 6px 5px rgba(0, 0, 0, 0.478);
}

.overlay {
  position: absolute;
  text-align: start;
  font-family: fantasy;
  font-size: x-large;
  margin: .5em;
  bottom: 0;
  color: rgb(240, 234, 244);
  text-shadow: 1px 1px 2px rgb(0, 0, 0);
  width: 50%;
  overflow: hidden;
  text-overflow: ellipsis;
}
</style>
