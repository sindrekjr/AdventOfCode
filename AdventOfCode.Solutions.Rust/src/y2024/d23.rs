use std::collections::{HashMap, HashSet};

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day23::solve_part_one(input),
        Part::P2 => Day23::solve_part_two(input),
    }
}

struct Day23;
impl Solution for Day23 {
    fn solve_part_one(input: String) -> String {
        let connections = parse_connections(&input);

        connections
            .iter()
            .filter(|(cpu, _)| cpu.starts_with('t'))
            .fold(HashSet::new(), |mut sets, (cpu, conns)| {
                for second in conns {
                    for third in &connections[second] {
                        if connections[third].contains(cpu) {
                            let mut group = vec![cpu, second, third];
                            group.sort();
                            sets.insert(group);
                        }
                    }
                }

                sets
            })
            .len()
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        let connections = parse_connections(&input);

        connections.keys().filter(|cpu| cpu.starts_with('t')).fold(
            String::default(),
            |password, cpu| {
                let mut group = the_gathering(cpu, HashSet::new(), &connections)
                    .iter()
                    .cloned()
                    .collect::<Vec<_>>();
                group.sort();

                let group_pass = group.join(",");
                if group_pass.len() > password.len() {
                    group_pass
                } else {
                    password
                }
            },
        )
    }
}

fn parse_connections(input: &str) -> HashMap<&str, Vec<&str>> {
    let mut connections = HashMap::new();
    input.lines().for_each(|conn| {
        let (a, b) = conn.split_once('-').unwrap();
        connections.entry(a).or_insert(vec![]).push(b);
        connections.entry(b).or_insert(vec![]).push(a);
    });

    connections
}

fn the_gathering<'a>(
    cpu: &'a str,
    group: HashSet<&'a str>,
    connections: &'a HashMap<&str, Vec<&str>>,
) -> HashSet<&'a str> {
    let cpu_conns = &connections[cpu];
    if group.iter().any(|member| !cpu_conns.contains(member)) {
        return group;
    }

    let mut group = group.clone();
    group.insert(cpu);

    for conn in cpu_conns {
        group = the_gathering(conn, group, connections);
    }

    group
}
