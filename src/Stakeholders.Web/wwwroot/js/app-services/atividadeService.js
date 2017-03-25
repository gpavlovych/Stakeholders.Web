
porlaDashboard.factory('$atividadeService', function () {
    var atividades = [
        {
            title: 'Lorem Ipsum Task',
            content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
            user: 'John Appleseed',
            relatedtogoal: 'Loren goal',
            createdyear: 2017,
            createdmonth: 01,
            createdday: 01
        },
        {
            title: 'Lorem Ipsum Task',
            content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
            user: 'John Appleseed',
            relatedtogoal: 'Loren goal',
            createdyear: 2017,
            createdmonth: 01,
            createdday: 01
        },
        {
            title: 'Lorem Ipsum Task',
            content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
            user: 'John Appleseed',
            relatedtogoal: 'Loren goal',
            createdyear: 2017,
            createdmonth: 01,
            createdday: 01
        },
        {
            title: 'Lorem Ipsum Task',
            content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
            user: 'John Appleseed',
            relatedtogoal: 'Loren goal',
            createdyear: 2017,
            createdmonth: 01,
            createdday: 01
        },
        {
            title: 'Lorem Ipsum Task',
            content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
            user: 'John Appleseed',
            relatedtogoal: 'Loren goal',
            createdyear: 2017,
            createdmonth: 01,
            createdday: 01
        },
        {
            title: 'Lorem Ipsum Task',
            content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
            user: 'John Appleseed',
            relatedtogoal: 'Loren goal',
            createdyear: 2017,
            createdmonth: 01,
            createdday: 01
        },
        {
            title: 'Lorem Ipsum Task',
            content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
            user: 'John Appleseed',
            relatedtogoal: 'Loren goal',
            createdyear: 2017,
            createdmonth: 01,
            createdday: 01
        },
        {
            title: 'Lorem Ipsum Task',
            content: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt.',
            user: 'John Appleseed',
            relatedtogoal: 'Loren goal',
            createdyear: 2017,
            createdmonth: 01,
            createdday: 01
        }
    ];
    return {
        obterAtividades: function () {
            return atividades;
        },
        adicionarAtividade: function (obj) {
            atividades.push(obj);
        },
        removerAtividade: function (obj) {
            atividades.pop();
        },
    };
});