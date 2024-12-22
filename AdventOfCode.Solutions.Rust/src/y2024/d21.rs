use std::collections::HashMap;

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day21::solve_part_one(input),
        Part::P2 => Day21::solve_part_two(input),
    }
}

const DIRECTIONAL_MAP: [((char, char), &str); 145] = [
    (('0', '0'), ""),
    (('0', '1'), "^<"),
    (('0', '2'), "^"),
    (('0', '3'), "^>"),
    (('0', '4'), "^^<"),
    (('0', '5'), "^^"),
    (('0', '6'), "^^>"),
    (('0', '7'), "^^^<"),
    (('0', '8'), "^^^"),
    (('0', '9'), "^^^>"),
    (('0', 'A'), ">"),
    (('1', '0'), ">v"),
    (('1', '1'), ""),
    (('1', '2'), ">"),
    (('1', '3'), ">>"),
    (('1', '4'), "^"),
    (('1', '5'), "^>"),
    (('1', '6'), "^>>"),
    (('1', '7'), "^^"),
    (('1', '8'), "^^>"),
    (('1', '9'), "^^>>"),
    (('1', 'A'), ">>v"),
    (('2', '0'), "v"),
    (('2', '1'), "<"),
    (('2', '2'), ""),
    (('2', '3'), ">"),
    (('2', '4'), "<^"),
    (('2', '5'), "^"),
    (('2', '6'), "^>"),
    (('2', '7'), "<^^"),
    (('2', '8'), "^^"),
    (('2', '9'), "^^>"),
    (('2', 'A'), "v>"),
    (('3', '0'), "<v"),
    (('3', '1'), "<<"),
    (('3', '2'), "<"),
    (('3', '3'), ""),
    (('3', '4'), "<<^"),
    (('3', '5'), "<^"),
    (('3', '6'), "^"),
    (('3', '7'), "<<^^"),
    (('3', '8'), "<^^"),
    (('3', '9'), "^^"),
    (('3', 'A'), "v"),
    (('4', '0'), ">vv"),
    (('4', '1'), "v"),
    (('4', '2'), "v>"),
    (('4', '3'), "v>>"),
    (('4', '4'), ""),
    (('4', '5'), ">"),
    (('4', '6'), ">>"),
    (('4', '7'), "^"),
    (('4', '8'), "^>"),
    (('4', 'A'), ">>vv"),
    (('5', '0'), "vv"),
    (('5', '1'), "<v"),
    (('5', '2'), "v"),
    (('5', '3'), "v>"),
    (('5', '4'), "<"),
    (('5', '5'), ""),
    (('5', '6'), ">"),
    (('5', '7'), "<^"),
    (('5', '8'), "^"),
    (('5', '9'), "^>"),
    (('5', 'A'), "vv>"),
    (('6', '0'), "<vv"),
    (('6', '1'), "<<v"),
    (('6', '2'), "<v"),
    (('6', '3'), "v"),
    (('6', '4'), "<<"),
    (('6', '5'), "<"),
    (('6', '6'), ""),
    (('6', '7'), "<<^"),
    (('6', '8'), "<^"),
    (('6', '9'), "^"),
    (('6', 'A'), "vv"),
    (('7', '0'), ">vvv"),
    (('7', '1'), "vv"),
    (('7', '2'), "vv>"),
    (('7', '3'), "vv>>"),
    (('7', '4'), "v"),
    (('7', '5'), "v>"),
    (('7', '6'), "v>>"),
    (('7', '7'), ""),
    (('7', '8'), ">"),
    (('7', '9'), ">>"),
    (('7', 'A'), ">>vvv"),
    (('8', '0'), "vvv"),
    (('8', '1'), "<vv"),
    (('8', '2'), "vv"),
    (('8', '3'), "vv>"),
    (('8', '4'), "<v"),
    (('8', '5'), "v"),
    (('8', '6'), "v>"),
    (('8', '7'), "<"),
    (('8', '8'), ""),
    (('8', '9'), ">"),
    (('8', 'A'), "vvv>"),
    (('9', '0'), "<vvv"),
    (('9', '1'), "<<vv"),
    (('9', '2'), "<vv"),
    (('9', '3'), "vv"),
    (('9', '4'), "<<v"),
    (('9', '5'), "<v"),
    (('9', '6'), "v"),
    (('9', '7'), "<<"),
    (('9', '8'), "<"),
    (('9', '9'), ""),
    (('9', 'A'), "vvv"),
    (('A', '0'), "<"),
    (('A', '1'), "^<<"),
    (('A', '2'), "<^"),
    (('A', '3'), "^"),
    (('A', '4'), "^^<<"),
    (('A', '5'), "<^^"),
    (('A', '6'), "^^"),
    (('A', '7'), "^^^<<"),
    (('A', '8'), "<^^^"),
    (('A', '9'), "^^^"),
    (('A', 'A'), ""),
    (('^', 'A'), ">"),
    (('^', '<'), "v<"),
    (('^', 'v'), "v"),
    (('^', '>'), "v>"),
    (('^', '^'), ""),
    (('A', '^'), "<"),
    (('A', '<'), "v<<"),
    (('A', 'v'), "<v"),
    (('A', '>'), "v"),
    (('A', 'A'), ""),
    (('<', '^'), ">^"),
    (('<', 'A'), ">>^"),
    (('<', 'v'), ">"),
    (('<', '>'), ">>"),
    (('<', '<'), ""),
    (('v', '^'), "^"),
    (('v', 'A'), "^>"),
    (('v', '<'), "<"),
    (('v', '>'), ">"),
    (('v', 'v'), ""),
    (('>', '^'), "<^"),
    (('>', 'A'), "^"),
    (('>', '<'), "<<"),
    (('>', 'v'), "<"),
    (('>', '>'), ""),
];

struct Day21;
impl Solution for Day21 {
    fn solve_part_one(input: String) -> String {
        let map: HashMap<(char, char), &str> = DIRECTIONAL_MAP.into();
        input
            .lines()
            .map(|code| {
                let len = (0..3)
                    .fold(code.to_string(), |code, _| direct_robot(&map, &code))
                    .len();
                let num = code[..code.len() - 1].parse::<usize>().unwrap();

                len * num
            })
            .sum::<usize>()
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        String::new()
    }
}

fn direct_robot(map: &HashMap<(char, char), &str>, target_sequence: &str) -> String {
    target_sequence
        .chars()
        .fold(('A', String::new()), |(prev_ch, seq), next_ch| {
            (next_ch, format!("{}{}A", seq, map[&(prev_ch, next_ch)]))
        })
        .1
}
