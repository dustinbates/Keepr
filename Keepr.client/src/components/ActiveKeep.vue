<template>
  <div class="modal fade" id="activeKeep" tabindex="-1" data-bs-keyboard="false" role="dialog" aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-xl" role="document">
      <div class="modal-content">
        <div class="row">
          <div class="col-12 col-md-6">
            <img :src="activeKeep?.img" :alt="activeKeep?.name">
          </div>
          <div class="col-12 col-md-6 px-5 py-3 d-flex flex-column justify-content-evenly">
            <div class="row">
              <div class="col-12">
                <p class="fs-2 m-0 p-0 text-center">
                  <i class="mdi mdi-eye-outline" title="View count"></i> {{ activeKeep?.views }}
                  <i class="ms-5 mdi mdi-alpha-k-box-outline" title="Kept count"></i> {{ activeKeep?.kept }}
                </p>
              </div>
            </div>
            <div class="row">
              <div class="col-12 p-5">
                <h1 class="text-center">{{ activeKeep?.name }}</h1>
                <p class="text-start">{{ activeKeep?.description }}</p>
              </div>
            </div>
            <div class="row align-items-center">
              <div class="col-6">
                <div v-if="activeKeep?.vaultKeepId && activeVault.creatorId == account.id">
                  <button class="btn btn-outline-secondary" @click="deleteVaultKeep(activeKeep?.vaultKeepId)">
                    Remove <i class="mdi mdi-cancel"></i>
                  </button>
                </div>
                <div v-else-if="account.id" class="dropdown dropup ">
                  <button type="button" class="btn btn-dark dropdown-toggle" data-bs-toggle="dropdown"
                    aria-expanded="false" @click="getMyVaults">
                    Add to Vault
                  </button>
                  <ul class="dropdown-menu">
                    <li v-if="myVaults.length == 0" class="droptown-item">You have no Vaults</li>
                    <div v-for="v in myVaults">
                      <li class="dropdown-item" @click="createVaultKeep(v.id)">{{ v.name }}</li>
                    </div>
                  </ul>
                </div>
              </div>
              <div class="col-6">
                <div v-if="activeKeep" class="d-flex align-items-center justify-content-evenly">
                  <router-link :to="{ name: 'Profile', params: { profileId: activeKeep?.creator.id } }">
                    <img class="creatorImg" :src="activeKeep?.creator.picture" :alt="activeKeep?.creator.name"
                      data-bs-dismiss="modal">
                  </router-link>
                  <p class="fs-4 m-0 p-0">{{ activeKeep?.creator.name }}</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { accountService } from '../services/AccountService';
import { vaultKeepsService } from '../services/VaultKeepsService'
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
export default {
  setup() {
    return {
      account: computed(() => AppState.account),
      activeKeep: computed(() => AppState.activeKeep),
      keep: computed(() => AppState.keeps),
      myVaults: computed(() => AppState.myVaults),
      activeVault: computed(() => AppState.activeVault),
      async getMyVaults() {
        try {
          await accountService.getMyVaults()
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      },
      async createVaultKeep(id) {
        try {
          const keepId = this.activeKeep.id
          const vaultId = id
          logger.log(keepId, vaultId)
          const vaultKeepData = { keepId: keepId, vaultId: vaultId }
          await vaultKeepsService.createVaultKeep(vaultKeepData)
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      },
      async deleteVaultKeep(vaultKeepId) {
        try {
          if (await Pop.confirm('Delete this Keep?', "Are you sure you want to delete this Keep?", 'Delete', 'question')) {
            vaultKeepsService.deleteVaultKeep(vaultKeepId)
          }
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
img {
  height: 100%;
  width: 100%;
  object-fit: cover;
}

.creatorImg {
  height: 40px;
  width: 40px;
  border-radius: 50%;
}
</style>