<script language="ts">

    let { data } = $props()
</script>

<div>
    <h1>{data.user.email}</h1>
    <h3>Role: {data.user.roles[0]}</h3>
</div>