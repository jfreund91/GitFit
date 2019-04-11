<template>    
<div>
    <modal name="hello-world">
        hello, world!
    </modal>
    <button value="Open-modal" v-on:click="show">Open-modal</button>
</div>
</template>

<script>
export default{
    data() {
        return {

        }
    },
    methods: {
    show () {
        this.$modal.show('hello-world');
    },
    hide () {
        this.$modal.hide('hello-world');
    }
    }
}
</script>

<style>


</style>